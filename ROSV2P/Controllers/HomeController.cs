using System.Web.Mvc;
using ROSV2P.Models;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Configuration;
using System.DirectoryServices;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System;
using ROSV2P.Log4net;
using System.Web.UI;

namespace ROSV2P.Controllers
{
    public class HomeController : Controller
    {
        public static string ldapConfig = ConfigurationManager.AppSettings["ldapConfig"].ToString();
        public static string ldapDomain = ConfigurationManager.AppSettings["ldapDomain"].ToString();        
        public static string Users = ConfigurationManager.AppSettings["user"].ToString();
        public static string Pwrd = ConfigurationManager.AppSettings["password"].ToString();
        public static string version1 = ConfigurationManager.AppSettings["WiseURLv1"].ToString();
        public static string version2 = ConfigurationManager.AppSettings["WiseURLv2"].ToString();
        public static string V_URL= ConfigurationManager.AppSettings["ViewerURL"].ToString();
        public static string V_OutputPath = ConfigurationManager.AppSettings["ViewerOutputPath"].ToString();
        public static string F_Output = ConfigurationManager.AppSettings["Output"].ToString();        

        // GET: Home
        public ActionResult Home()
        {
            if (Session["User"] != null)
            {
                if (Session["objId"] != null)
                {
                    if (Session["version"] != "S")
                    {
                        Session["version"] = "C";                        
                    }
                    genGUID();
                    ViewBag.GUID = Session["GUID"];
                    ViewBag.version = Session["version"];
                    ViewBag.Objid = Session["objId"];
                    ViewBag.User = Session["User"].ToString().ToUpper();
                    return View();
                }
                else
                {
                    ViewBag.sUser = Session["User"].ToString().ToUpper();
                    return RedirectToAction("Search");
                }
            }
            else
                return RedirectToAction("Index");

        }

        public ActionResult Index()
        {
            string ObjID = string.Empty;
            ObjID = Request.QueryString["objID"];
            if (ObjID != null)
            {
                //Session["version"] = "";
                return View();
            }
            if (Session["objId"] != null && Session["User"] != null)
                return RedirectToAction("Home");
            else
            if (Session["User"] != null)
            {
                if (Session["objId"] == null)
                {
                    Utility.LogMessage("Application Logged In.", Utility.LogLevel.INFO);
                    Utility.LogMessage("Current user : " + Session["User"], Utility.LogLevel.INFO);
                    ViewBag.sUser = Session["User"];
                    return RedirectToAction("Search");
                }
                else
                    return RedirectToAction("Home");
            }
            else
                return View();
        }

        public ActionResult Search()
        {        
            if (Session["objId"] == null && Session["User"] == null)
                return RedirectToAction("Index");
            else
            {                
                if (Session["objId"] != null && Session["User"] != null)
                {                    
                    genGUID();
                    return RedirectToAction("Home");
                }
                else
                {
                    if (Session["User"] != null && Session["objId"] == null)
                    {
                        ViewBag.sUser = Session["User"];
                        return View();
                    }
                    else
                        return RedirectToAction("Index");
                }
            }
        }

        public ActionResult ROSWrapper()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Authentication(GetUserAccessInfo getObj)
        {
            GetUserAccessInfo _UserIfo = new GetUserAccessInfo();
            string aPwrd = string.Empty;
            if (getObj._Password == null)
                getObj._Password = Pwrd;
            getObj.Config_User = Users;
            Utility.LogMessage("Validating User", Utility.LogLevel.INFO);
            _UserIfo = ValidateLogin(getObj);
            if (_UserIfo._Status == "1")
            {
                if (getObj._objID != null)
                    Session["objId"] = getObj._objID;
                else
                    Session["objId"] = null;
                Session["User"] = _UserIfo._AWDusername;
            }
            return Json(_UserIfo, JsonRequestBehavior.AllowGet);
        }

        public GetUserAccessInfo ValidateLogin(GetUserAccessInfo getObj)
        {            
            string User = string.Empty;
            string passWord = string.Empty;
            string GpUser = string.Empty; //User to check in ROS group
            if (getObj._AppVersion == "cApp")
            {
                User = getObj.Config_User.Trim();
                passWord = getObj._Password;
                GpUser = CurrentUser(System.Web.HttpContext.Current.User.Identity.Name);                
            }
            else
            {                
                User = getObj._AWDusername;
                passWord = getObj._Password;
                GpUser = getObj._AWDusername;
            }
            try
            {
                DirectoryEntry entry = new DirectoryEntry(ldapConfig, ldapDomain + User.Trim(), passWord.Trim());
                try
                {
                    object obj = entry.NativeObject;
                    getObj._Status = "valid";
                }
                catch { getObj._Status = "0"; }
                if (getObj._Status == "valid")
                {
                    DirectorySearcher mySearcher = new DirectorySearcher(entry);
                    mySearcher.Filter = "(&(objectClass=user)(|(cn=" + GpUser + ")(sAMAccountName=" + GpUser + ")))";
                    mySearcher.PropertiesToLoad.Add("givenName");
                    mySearcher.PropertiesToLoad.Add("sn");                    
                    mySearcher.PropertiesToLoad.Add("mail");
                    mySearcher.PropertiesToLoad.Add("memberOf");
                    SearchResult result = mySearcher.FindOne();

                    foreach (string GroupPath in result.Properties["memberOf"])
                    {
                        if (GroupPath.Contains(ConfigurationManager.AppSettings["ldapGroup"].ToString()))
                        {
                            getObj._Status = "1";
                            //Utility.LogMessage("IsInADGroup: Information:" + GpUser + " is a valid member", Utility.LogLevel.DEBUG);
                            break;
                        }
                        getObj._Status = "2";
                    }
                    if (result != null)
                    {
                        getObj._Email = result.Properties["mail"][0].ToString();
                        getObj._FullName = result.Properties["givenName"][0].ToString();
                        getObj._FullName = getObj._FullName + " " + result.Properties["sn"][0].ToString();                                                                       
                    }
                }
            }
            catch(Exception ex)
            {                
                if (!ex.Message.Contains("Index was out of range"))
                    getObj._Status = "0";
                Utility.LogMessage("IsInADGroup: Information:" + GpUser + " is a not valid member", Utility.LogLevel.DEBUG);
            }
            return getObj;
        }      

        public string LogOrderToDB(DBUpdationFields dbFieldsValue)
        {
            Utility.LogMessage(dbFieldsValue.reqType + " - order Logging to db", Utility.LogLevel.DEBUG);
            string Result = string.Empty;
            try
            {
                DBEngine db = new DBEngine();
                if (db.Insert(dbFieldsValue))
                {
                    Result = "success";
                    Utility.LogMessage("Logged to db Successfully.", Utility.LogLevel.DEBUG);
                }
                else
                    Result = "Fail";                
            }
            catch (Exception ex)
            {
                Utility.LogMessage("LogOrderToDB Method : " + ex.Message, Utility.LogLevel.ERROR);
            }
            return Result;
        }

        public string GenerateRpt(string FilePath, Array LabOneData, string reqType)
        {
            Utility.LogMessage("Generating IDX file...", Utility.LogLevel.DEBUG);
            
            string Result = string.Empty;
            try
            {
                Methods objMethods = new Methods();
                Utility.LogMessage("Building " + reqType + " Order...", Utility.LogLevel.INFO);
                if (objMethods.IdXCreation(FilePath, LabOneData, reqType))
                {
                    Result = "Success";
                }
                else
                    Result = "Fail";
            }
            catch (Exception ex)
            {
                Utility.LogMessage("GenerateRpt Method : " + ex.Message, Utility.LogLevel.ERROR);
            }
            return Result;
        }
                
        [HttpPost]
        public JsonResult GetObjectData(GetObjectDataRequestModel getObj)
        {
            Utility.LogMessage("Retrieving object id's [" + getObj.ObjId+"] LOB fields.", Utility.LogLevel.DEBUG);
            GetObjectDataResponseModel oResponse = new GetObjectDataResponseModel();
            String URL = version2;
            URL += "GetObjectData/" + getObj.ObjId + "/" + getObj.AppId;
            try
            {
                HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(URL);
                string ResponseString = string.Empty;
                oRequest.Method = "GET";
                oRequest.ContentType = "application/json; charset=utf-8";
                oRequest.Timeout = Int32.MaxValue;
                var Response = (HttpWebResponse)oRequest.GetResponse();
                if (Response.StatusCode != HttpStatusCode.OK)
                {
                    var iReader = new StreamReader(Response.GetResponseStream());
                    string response = iReader.ReadToEnd();
                }
                var oResponseStream = Response.GetResponseStream();
                var oReader = new StreamReader(oResponseStream);
                ResponseString = oReader.ReadToEnd();
                JavaScriptSerializer oSerializer = new JavaScriptSerializer();
                oSerializer.MaxJsonLength = Int32.MaxValue;
                oResponse = oSerializer.Deserialize<GetObjectDataResponseModel>(ResponseString);
                Utility.LogMessage("Retrieved required Lob fields ", Utility.LogLevel.INFO);
            }
            catch (Exception ex)
            {
                oResponse.Response = new ResponseMessage();
                oResponse.Response.Code = 0;
                oResponse.Response.Message = ex.Message;
                oResponse.Response.Severity = "Error";
                Utility.LogMessage("GetObjectData Method : " + ex.Message, Utility.LogLevel.ERROR);

            }            
            return Json(oResponse, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult AddComments(PostCommentsRequestModel oModel)
        {
            Utility.LogMessage(oModel.cmdType + " Comment added to work " + oModel.Comment, Utility.LogLevel.DEBUG);
            Utility.LogMessage("Adding Comments to work...", Utility.LogLevel.INFO);
            PostCommentsResponseModel oResponseMessage = new PostCommentsResponseModel();
            string URL = version2 + "PostComment";

            HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(URL);
            oRequest.Method = "POST";
            oRequest.ContentType = "application/json; charset=utf-8";
            oRequest.Timeout = Int32.MaxValue;
            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            try
            {
                string strRequest = oSerializer.Serialize(oModel);
                using (StreamWriter oWriter = new StreamWriter(oRequest.GetRequestStream()))
                {
                    oWriter.Write(strRequest);
                }

                System.Net.ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate
                            certificate, X509Chain chain,
                            SslPolicyErrors sslPolicyErrors)
                    { return true; };

                WebResponse oResponse = oRequest.GetResponse();
                Stream oStream = oResponse.GetResponseStream();
                StreamReader oReader = new StreamReader(oStream);
                string oResultStr = oReader.ReadToEnd();

                JavaScriptSerializer resSerializer = new JavaScriptSerializer();
                resSerializer.MaxJsonLength = Int32.MaxValue;
                //oResponseMessage = resSerializer.Deserialize<PostCommentsResponseModel>(oResultStr);

                if (oResponse != null)
                {
                    oResponseMessage = resSerializer.Deserialize<PostCommentsResponseModel>(oResultStr);
                }
                Utility.LogMessage("Comments successfully added to work.", Utility.LogLevel.INFO);
            }
            catch (Exception ex)
            {
                Utility.LogMessage("AddComments Method : " + ex.Message, Utility.LogLevel.ERROR);
            }
            return Json(oResponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FindChildren(FindChildrenRequestModel fcModel)
        {
            Utility.LogMessage("Finding " + fcModel.ObjId + " object id's Children.", Utility.LogLevel.DEBUG);
            FindChildrenResponseModel oResponse = new FindChildrenResponseModel();
            string URL = version2;            
            URL += "FindChildren/" + fcModel.ObjId + "/" + fcModel.AppId;
            if (fcModel.UserId != "" && fcModel.UserId != null)
            {
                URL += "/" + fcModel.UserId;
            }
            try
            {
                HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(URL);

                string ResponseString = string.Empty;

                oRequest.Method = "GET";
                oRequest.ContentType = "application/json; charset=utf-8";
                oRequest.Timeout = Int32.MaxValue;

                var Response = (HttpWebResponse)oRequest.GetResponse();
                if (Response.StatusCode != HttpStatusCode.OK)
                {
                    var iReader = new StreamReader(Response.GetResponseStream());
                    string response = iReader.ReadToEnd();
                }

                var oResponseStream = Response.GetResponseStream();
                var oReader = new StreamReader(oResponseStream);
                ResponseString = oReader.ReadToEnd();
                Int64 length = ResponseString.Length;

                JavaScriptSerializer oSerializer = new JavaScriptSerializer();
                oSerializer.MaxJsonLength = Int32.MaxValue;
                oResponse = oSerializer.Deserialize<FindChildrenResponseModel>(ResponseString);
                Utility.LogMessage("Find children json output: " + ResponseString, Utility.LogLevel.DEBUG);
            }
            catch (Exception ex)
            {
                Utility.LogMessage("FindChildren Method : " + ex.Message, Utility.LogLevel.ERROR);
            }
            return Json(oResponse, JsonRequestBehavior.AllowGet);
        }

        public string GetDocumentBase64(GetObjectDataRequestModel gdModel)
        {
            Utility.LogMessage("Image retrivel for " + gdModel.ObjId + " object id", Utility.LogLevel.DEBUG);
            GetDocumentResponseModel oResponse = new GetDocumentResponseModel();
            string URL = version2;
            //URL += "GetDocument/" + ObjID + "/" + AppID + "/" + UserID;
            URL += "GetDocumentBase64/" + gdModel.ObjId + "/" + gdModel.AppId;
            if (gdModel.UserId != "" && gdModel.UserId != null)
            {
                URL += "/" + gdModel.UserId;
            }
            string result = string.Empty;
            try
            {
                HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(URL);

                string ResponseString = string.Empty;

                oRequest.Method = "GET";
                oRequest.ContentType = "application/json; charset=utf-8";
                oRequest.Timeout = Int32.MaxValue;

                var Response = (HttpWebResponse)oRequest.GetResponse();
                if (Response.StatusCode != HttpStatusCode.OK)
                {
                    var iReader = new StreamReader(Response.GetResponseStream());
                    string response = iReader.ReadToEnd();
                }

                var oResponseStream = Response.GetResponseStream();
                var oReader = new StreamReader(oResponseStream);
                ResponseString = oReader.ReadToEnd();
                Int64 length = ResponseString.Length;

                JavaScriptSerializer oSerializer = new JavaScriptSerializer();
                oSerializer.MaxJsonLength = Int32.MaxValue;
                oResponse = oSerializer.Deserialize<GetDocumentResponseModel>(ResponseString);
                result = oResponse.DocumentContent;
            }
            catch (Exception ex)
            {
                //oResponse.Response = new ResponseMessage();
                //oResponse.Response.Code = 0;
                //oResponse.Response.Message = ex.Message;
                //oResponse.Response.Severity = "Error";
                Utility.LogMessage("GetDocumentBase64 Method : " + ex.Message, Utility.LogLevel.ERROR);
                result = ex.Message;
            }
            return result;
        }

        [HttpPost]
        public JsonResult SearchWorkitem (SearchRequestModel SearchReq, string SearchLOBValues)
        {
            Methods objMethods = new Methods();
            Utility.LogMessage("Search invoked", Utility.LogLevel.INFO);
            SearchResponseModel sResponse = new SearchResponseModel();            
            SearchReq.AppId = SearchReq.AppId;
            SearchReq.UserId= string.IsNullOrWhiteSpace(SearchReq.UserId) ? string.Empty : SearchReq.UserId;
           // SearchReq.Date= string.IsNullOrWhiteSpace(SearchReq.Date) ? string.Empty : SearchReq.Date;
            int i = 0;
            SearchReq.PageNumber= true ? (int.TryParse(SearchReq.PageNumber.ToString(), out i) ? int.Parse(SearchReq.PageNumber.ToString()) : 0) : 0;
            SearchReq.Fields = objMethods.createIndexFields(SearchLOBValues);
            var dFields = "";
            for (int key = 0; key < SearchReq.Fields.Count; key++)
            {
                dFields = SearchReq.Fields[key].FieldName +" : "+ SearchReq.Fields[key].FieldValue+" ";
            }
            Utility.LogMessage("Searching Inputs - AppId : "+SearchReq.AppId+" SearchContext: "+SearchReq.SearchContext+" Fields : "+ dFields, Utility.LogLevel.INFO);
            string URL = version2 + "SearchWithDateRange";
            HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(URL);
            oRequest.Method = "POST";
            oRequest.ContentType = "application/json; charset=utf-8";
            oRequest.Timeout = Int32.MaxValue;

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            try
            {
                string strRequest = oSerializer.Serialize(SearchReq);
                using (StreamWriter oWriter = new StreamWriter(oRequest.GetRequestStream()))
                {
                    oWriter.Write(strRequest);
                }

                System.Net.ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate
                            certificate, X509Chain chain,
                            SslPolicyErrors sslPolicyErrors)
                    { return true;};

                WebResponse oResponse = oRequest.GetResponse();
                Stream oStream = oResponse.GetResponseStream();
                StreamReader oReader = new StreamReader(oStream);
                string oResultStr = oReader.ReadToEnd();

                JavaScriptSerializer resSerializer = new JavaScriptSerializer();
                resSerializer.MaxJsonLength = Int32.MaxValue;

                if (oResponse != null)
                {
                    sResponse = resSerializer.Deserialize<SearchResponseModel>(oResultStr);
                }
            }
            catch (Exception ex)
            {
                Utility.LogMessage("SearchWorkitem Method - "+ex.Message, Utility.LogLevel.ERROR);
                sResponse.Results = null;           
            }
            return Json(sResponse, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public string soapRequestMVR(string rqData)
        {
            Utility.LogMessage("MVR soap request input : " + rqData, Utility.LogLevel.DEBUG);
            string Status = string.Empty;
            try
            {
                Methods objMethod = new Methods();
                Status = objMethod.SendingMVRSoapRequest(rqData);                
            }
            catch (Exception ex)
            {
                Utility.LogMessage("soapRequestMVR Method : " + ex.Message, Utility.LogLevel.ERROR);                
            }
            return Status;
        }

        [ValidateInput(false)]
        public string soapRequestIR(string reqData)
        {
            Utility.LogMessage("IR soap request input : " + reqData, Utility.LogLevel.DEBUG);
            string Status = string.Empty;
            try
            {
                Methods objMethod = new Methods();
                Status = objMethod.SendingIRSoapRequest(reqData);                
            }
            catch (Exception ex)
            {
                Utility.LogMessage("soapRequestIR Method : " + ex.Message, Utility.LogLevel.ERROR);
            }
            return Status;
        }

        [HttpPost]
        public string GenerateAPStiff(GetTiffDataRequestModel gdModel)
        {
            Utility.LogMessage("Fetching APS Tiff file from shared location..", Utility.LogLevel.INFO);
            string Result = string.Empty;
            Methods objMethod = new Methods();
            try
            {                
                if (objMethod.fetchingTiff(gdModel))
                {
                    Result = "success";
                    Utility.LogMessage("APS Tiff file fetched Successfully.", Utility.LogLevel.INFO);
                }
                else
                    Result = "Fail";                
            }
            catch (Exception ex)
            {
                Utility.LogMessage("GenerateAPStiff Method : " + ex.Message, Utility.LogLevel.ERROR);
            }
            return Result;
        }

        [HttpPost]
        public string genGUID()
        {
            String guid = Guid.NewGuid().ToString();
            if (guid != null && guid != "")
            {
                Session["GUID"] = guid;
                return guid;
            }
            else
                return "";
        }

        public bool sessionData(string objId)
        {
            Session["objId"] = objId;
            Session["version"] = "S";            
            return true;
        }

        [HttpPost]
        public bool sessionClear()
        {
            Session["objId"] = null;
            Session["version"] = null;
            Session["User"] = null;
            Utility.LogMessage("Application Logged out", Utility.LogLevel.INFO);
            return true;
        }

        [HttpPost]
        public string clearObjid()
        {
            Session["objId"] = null;
            Session["GUID"] = null;
            if (Session["version"] == "S")
                return "Y";
            else
                return "N";            
        }

        [HttpPost]
        public bool FileCheck(string tiffURL)
        {
            Methods m = new Methods();
            if (m.fileExists(tiffURL))
                return true;
            else
                return false;
        }

        [HttpPost]
        public JsonResult GetConfigURL()
        {
            ConfigURL sResponse = new ConfigURL();
            sResponse.TiffPath = V_OutputPath;
            sResponse.OutputPath = F_Output;
            sResponse.Viewer = V_URL;
            return Json(sResponse, JsonRequestBehavior.AllowGet);
        }

        public string CurrentUser(string User)
        {
            string C_User = string.Empty;
            if(User.IndexOf("\\") != -1)
            {
                var cSplit = User.Split('\\');
                C_User = cSplit[1];
            }

            return C_User;
        }

    }
}