using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using ROSV2P.Log4net;

namespace ROSV2P.Models
{
    public class DBEngine
    {

        OracleConnection con;
        public static string O_Connectionstring = ConfigurationManager.ConnectionStrings["OracleConnection"].ToString();

        void Connect()
        {
            try
            {
                con = new OracleConnection();                
                con.ConnectionString = O_Connectionstring;
                con.Open();
                Utility.LogMessage("Db Configuration String: "+ O_Connectionstring, Utility.LogLevel.DEBUG);
            }
            catch (Exception ex)
            {
                Utility.LogMessage(ex.Message, Utility.LogLevel.ERROR);
                throw ex;
            }
        }

        void Close()
        {
            try
            {
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Insert(DBUpdationFields dbFieldsValue)
        {
            try
            {                
                string oQuery = "insert into APS_ORDER (CreateTime,Quoteback,PolicyNum,InsLastName,Req_Type,AgentNumber,AgencyNumber) values(" +
                                    "'" + dbFieldsValue.dNow + "','" + dbFieldsValue.sQuoteBack + "','" + dbFieldsValue.policyNumber + "','" + dbFieldsValue.lastName + "','" + dbFieldsValue.reqType + "','" + dbFieldsValue.AgentNumber + "','" + dbFieldsValue.AgencyNumber + "')";
                Connect();
                OracleCommand cmd = new OracleCommand(oQuery, con);
                cmd.ExecuteNonQuery();
                Close();
                return true;
            }
            catch(Exception ex)
            {
                Utility.LogMessage("Insert Method : " + ex.Message, Utility.LogLevel.ERROR);
                return false;
            }
        }

    }
    public class DBUpdationFields
    {
        public string dNow { get; set; }
        public string policyNumber { get; set; }
        public string lastName { get; set; }
        public string sQuoteBack { get; set; }
        public string reqType { get; set; }
        public string AgentNumber { get; set; }
        public string AgencyNumber { get; set; }
    }
}