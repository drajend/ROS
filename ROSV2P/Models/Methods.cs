using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using ROSV2P.Log4net;
using System.Configuration;

namespace ROSV2P.Models
{
    public class Methods
    {        
        DataSet ds = new DataSet();
        public static string F_Output = ConfigurationManager.AppSettings["Output"].ToString();
        public string SendingMVRSoapRequest(string rqData)
        {            
            Utility.LogMessage("sendMVRRequest call started", Utility.LogLevel.DEBUG);
            string Status = string.Empty;
            try
            {                                
                ROSV2P.WISEService.VBWSAgentClient Proxy = new ROSV2P.WISEService.VBWSAgentClient();
                Status = Proxy.sendMVRRequest(rqData);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Utility.LogMessage("sendMVRRequest call Ended : " + Status, Utility.LogLevel.DEBUG);
            return Status;
        }
        public string SendingIRSoapRequest(string rqData)
        {
            Utility.LogMessage("sendIRRequest call started", Utility.LogLevel.DEBUG);
            string Status = string.Empty;
            try
            {
                ROSV2P.WISEService.VBWSAgentClient Proxy = new ROSV2P.WISEService.VBWSAgentClient();
                Status = Proxy.sendIRRequest(rqData);                
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            Utility.LogMessage("sendIRRequest call Ended : " + Status, Utility.LogLevel.DEBUG);
            return Status;
        }
        public bool IdXCreation(string path, Array LabOneData,string req)
        {
            Utility.LogMessage("Creating IDX file Destination : " + path, Utility.LogLevel.INFO);
            try
            {
                FileStream fs = null;
                if (!File.Exists(path))
                {
                    using (fs = File.Create(path))
                    {
                        fs.Close();
                        Utility.LogMessage("Writing " + req + " Order to disk...", Utility.LogLevel.INFO);
                        using (StreamWriter sw = new StreamWriter(path))
                        {
                            Utility.LogMessage("Writing LabOne data file to: " + path, Utility.LogLevel.DEBUG);
                            foreach (var Data in LabOneData)
                            {
                                sw.WriteLine(Data);
                            }
                        }
                    }
                }
                if (MovingFileToOutputPath(path,req))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Utility.LogMessage("IdXCreation Method : " + ex.Message, Utility.LogLevel.ERROR);
                return false;                
            }
        }
        public bool MovingFileToOutputPath(string path,string req)
        {
            string Extension = string.Empty;
            try
            {
                if (req == "APS")
                    Extension = ".idx";
                else
                    if (req == "ES")
                    Extension = ".esh";
                else
                    Extension = ".crt";
                string OutputPath = F_Output + Path.GetFileNameWithoutExtension(path) + Extension;
                Utility.LogMessage("Moving IDX file into " + path + " to " + OutputPath, Utility.LogLevel.DEBUG);
                File.Copy(path, OutputPath);
                if (File.Exists(path))
                    File.Delete(path);
                return true;
            }
            catch (Exception ex)
            {                
                Utility.LogMessage("MovingFileToOutputPath Method : " + ex.Message, Utility.LogLevel.ERROR);
                return false;                
            }
        }

        public List<IndexField> createIndexFields(string fields)
        {
            //Example:  ,-,FNAME,HEINZ,FIRST NAME
            //          ,-,LNAME,AYLLON,LAST NAME
            string replaceLOBFields = fields.Replace(",-,", ";");
            string[] parts = replaceLOBFields.Split(';');
            List<IndexField> listIndexFields = new List<IndexField>();
            foreach (string prt in parts)
            {
                if (prt != "")
                {
                    string[] prt2 = prt.Split(',');
                    IndexField newField = new IndexField();
                    newField.FieldName = prt2[0];
                    newField.FieldValue = prt2[1];
                    newField.LOBTranslation = prt2[2];
                    listIndexFields.Add(newField);
                }
            }
            return listIndexFields;
        }
        public bool fetchingTiff(GetTiffDataRequestModel gModel)
        {
            try
            {
                File.Copy(gModel.SharePath, gModel.DestPath);
                return true;
            }
            catch (Exception ex)
            {
                Utility.LogMessage("fetchingTiff Method : " + ex.Message, Utility.LogLevel.ERROR);
                return false;
            }
        }
        public bool fileExists(string tiffURL)
        {
            try
            {
                Utility.LogMessage("Filechecking - " + tiffURL, Utility.LogLevel.INFO);
                FileInfo file = new FileInfo(tiffURL);
                return file.Exists;
                
            }
            catch(Exception ex)
            {
                Utility.LogMessage("fileExists Method : " + ex.Message, Utility.LogLevel.ERROR);
                return false;
            }
        }
    }
}