using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ROSV2P.Models
{
    public class Declaration
    {
        //Login
        [Display(Name ="Username:")]
        public string lUser { get; set; }
        [Display(Name = "Password:")]
        public string lPassword { get; set; }

        //Search Page
        [Display(Name = "Work Type: ")]
        public string ddWorktype { get; set; }
        [Display(Name = "Search Type: ")]
        public string ddSearchtype { get; set; }


        //Main tab variables
        [Display(Name = "Name: ")]
        public string userName { get; set; }
        public string policyNumber { get; set; }
        [Display(Name = "APS - EMSI")]
        public bool chkAPSEMSI { get; set; }
        [Display(Name = " MVR - Choice Point")]
        public bool chkMVR { get; set; }
        [Display(Name = " IR - CRL Plus")]
        public bool chkIR { get; set; }
        [Display(Name = " A&H Inspection (Younger Ages)")]
        public bool chkAHinspection { get; set; }
        [Display(Name = " Did you check the Domino database and coversheet?")]
        public bool chkAPSdbSheet { get; set; }
        [Display(Name = " Did you check the coversheet?")]
        public bool chkIRcoverSheet { get; set; }
        [Display(Name = "Agent Number ")]
        public string txtAgentNumber { get; set; }
        [Display(Name = "Validated case for agent error?*")]
        public bool chkAPSAgentError { get; set; }
        [Display(Name = "Credit Report - EMSI")]
        public string rdCreditName { get; set; }
        [Display(Name = "ESearch - EMSI (Includes Credit Report)")]
        public string rdEsearchName { get; set; }
        public string rdCreditEsearch { set; get; }
        public string btnClear { get; set; }
        [Display(Name = "Policy No")]
        public string txtPolicyNo { get; set; }
        [Display(Name = "Policy Amount:")]
        public string txtPolicyAmount { get; set; }

        //Applicant Tab variables
        public string txtBirthDay { get; set; }
        [Display(Name = "Birth Date (mm/dd/yyyy):")]
        public string txtAppBirthDay { get; set; }
        [Display(Name = "Prefix:")]
        public string txtAppPrefix { get; set; }
        [Display(Name = "First Name:")]
        public string txtAppFname { get; set; }
        [Display(Name = "Last Name:")]
        public string txtAppLname { get; set; }
        [Display(Name = "Middle:")]
        public string txtAppMiddlename { get; set; }
        [Display(Name = "SSN:")]
        public string txtAppssn { get; set; }
        [Display(Name = "Driver's License Number:")]
        public string txtAppDriverLicNo { get; set; }
        [Display(Name = "Address:")]
        public string txtAppAddressFirst { get; set; }
        public string txtAppAddressSecond { get; set; }
        public string txtAppAddressThird { get; set; }
        [Display(Name = "City:")]
        public string txtAppCity { get; set; }
        [Display(Name = "Home Phone:")]
        public string txtAppHomePhone { get; set; }
        [Display(Name = "Zip:")]
        public string txtAppZip { get; set; }
        [Display(Name = "Work Phone:")]
        public string txtAppWorkPhone { get; set; }
        [Display(Name = "Suffix:")]
        public string ddAppSuffix { get; set; }
        public string txtAppSuffix { get; set; }
        [Display(Name = "Driver's License State:")]
        public string ddAppDriverLicState { get; set; }
        [Display(Name = "Gender:")]
        public string ddAppGender { get; set; }
        [Display(Name = "Marital Status:")]
        public string ddAppMaritalStatus { get; set; }
        [Display(Name = "State:")]
        public string ddAppState { get; set; }

        //Order Tab
        [Display(Name = "EMSI (All Other BGA's)")]
        public string rdorderEMSI { get; set; }
        [Display(Name = "SelectQuote (Only)")]
        public string rdorderSelectquote { get; set; }
        [Display(Name = "RXREVIEW/MIBEVAL")]
        public string rdorderRxreview { get; set; }
        [Display(Name = "Corporate Markets")]
        public string rdorderCorporateMarket { get; set; }
        [Display(Name = "Nashville EMSI")]
        public string rdorderNashville { get; set; }
        [Display(Name = "AIG Direct")]
        public string rdAigDirect { get; set; }
        [Display(Name = "Special Instructions for Order (90 characters maximum):")]
        public string txtareaorderSpecialinstr { get; set; }
        [Display(Name = "Rush Order")]
        public bool chkorderRushorder { get; set; }

        //Conatact tab variables
        [Display(Name = "Requestor Full Name:")]
        public string txtConRequestorName { get; set; }
        [Display(Name = "Requestor Phone Number:")]
        public string txtConRequestorPhone { get; set; }
        [Display(Name = "Requestor Email:")]
        public string txtConRequestorEmail { get; set; }
        [Display(Name = "First Name:")]
        public string txtConFirstName { get; set; }
        [Display(Name = "Middle:")]
        public string txtConMiddleName { get; set; }
        [Display(Name = "Last Name:")]
        public string txtConLastName { get; set; }
        [Display(Name = "Phone:")]
        public string txtConPhone { get; set; }
        [Display(Name = "Email:")]
        public string txtConEmail { get; set; }

        //Doctor tab 
        [Display(Name = "Hospital/Clinic Name:")]
        public string txtDocClinicName { get; set; }
        [Display(Name = "Patient Number (*req'd for Kaiser Permanente):")]
        public string txtDocPatientNumber { get; set; }
        [Display(Name = "Doctor's First Name:")]
        public string txtDocFirstName { get; set; }
        [Display(Name = "Doctor's Last Name:")]
        public string txtDocLastName { get; set; }
        [Display(Name = "Phone:")]
        public string txtDocPhone { get; set; }
        [Display(Name = "Doctor/Facility Address:")]
        public string txtDocAddress1 { get; set; }
        public string txtDocAddress2 { get; set; }
        [Display(Name = "City:")]
        public string txtDocCity { get; set; }
        [Display(Name = "State:")]
        public bool ddDocState { get; set; }
        [Display(Name = "Zip:")]
        public string txtDocZip { get; set; }
        [Display(Name = "Instructions to the Doctor:")]
        public string lstboxDocInstruction { get; set; }        
        public string ddDocInstructon { get; set; }

        public string lstDocPopList { get; set; }

        //Authorization tab
        [Display(Name = "*Select the Source That Contains the Authorization Form:")]
        public bool chkAuthSource1 { get; set; }
        [Display(Name = "*Page(s) Containing the Authorization Form:")]
        public string txtAuthPageForm { get; set; }

        //Additional Information tab

        [Display(Name = "Employer Name:")]
        public string txtAddEmpName { get; set; }
        [Display(Name = "Occupation:")]
        public string txtAddOccupation { get; set; }
        [Display(Name = "Employer Address Line 1:")]
        public string txtAddAddress1 { get; set; }
        [Display(Name = "Employer Address Line 2:")]
        public string txtAddAddress2 { get; set; }
        [Display(Name = "Employer Phone Number:")]
        public string txtAddPhone { get; set; }
        [Display(Name = "City:")]
        public string txtAddCity { get; set; }
        [Display(Name = "State:")]
        public string ddAddState { get; set; }
        [Display(Name = "Zip:")]
        public string txtAddZip { get; set; }
        [Display(Name = "Beneficiary First Name:")]
        public string txtAddBenificiaryFname { get; set; }
        [Display(Name = "Beneficiary Middle Name:")]
        public string txtAddBenificiaryMname { get; set; }
        [Display(Name = "Beneficiary Last Name:")]
        public string txtAddBenificiaryLname { get; set; }
        [Display(Name = "Agent Name:")]
        public string txtAddAgentName { get; set; }
        [Display(Name = "Agency Name:")]
        public string txtAddAgencyName { get; set; }
        [Display(Name = "Agency/Agent phone number:")]
        public string txtAddAgentPhone { get; set; }
        [Display(Name = "*Max length 25 characters")]
        public string txtAgentnameMsg { get; set; }
        [Display(Name = "Beneficiary Suffix:")]
        public string ddAddbeneficiarySuf { get; set; }
        [Display(Name = "Beneficiary Relationship:")]
        public string ddAddBeneficiaryRelationShip { get; set; }

        //Additional Reports and Supplements tab
        [Display(Name = " BBR")]
        public bool chkAddRepBBR { get; set; }
        [Display(Name = " Credit Check")]
        public bool chkAddRepCreditCheck { get; set; }
        [Display(Name = " Criminal Court Record")]
        public bool chkAddRepCriminalRec { get; set; }
        [Display(Name = "Special Attention/Additional Remarks")]
        public string TxtAreaAdditionalRemark { get; set; }

        //Instruction Modal tab
        [Display(Name = "Type the message to the Doctor here OR select a message from the list (Note 250 charaters maximum per line)")]
        public string ddInsDoctorMessage { get; set; }
        [Display(Name = "Instruction (Maximum of 99 lines or 8900 characters)")]
        public string lstDocInstruction { get; set; }

    }

    public class IndexField
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string LOBTranslation { get; set; }
    }
    public class ResponseMessage
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Severity { get; set; }
    }


    public class WISEObject
    {
        public string ObjectId { get; set; }
        public List<IndexField> IndexFields { get; set; }
        public List<IndexField> ObjectFields { get; set; }
    }

    public class FindChildrenRequestModel
    {
        public string ObjId { get; set; }
        public string AppId { get; set; }
        public string UserId { get; set; }
    }

    public class FindChildrenResponseModel
    {
        public List<string> ChildObjectIds { get; set; }
        public ResponseMessage Response { get; set; }
    }

    
    public class PostCommentsResponseModel
    {
        public ResponseMessage Response { get; set; }
    }
    public class GetObjectDataResponseModel
    {
        public string ObjId { get; set; }
        public List<IndexField> IndexFields { get; set; }
        public List<IndexField> ObjectFields { get; set; }
        public ResponseMessage Response { get; set; }
        public string URL { get; set; }
    }

    public class GetObjectDataRequestModel
    {
        public string ObjId { get; set; }

        public string AppId { get; set; }
        public string UserId { get; set; }
        public string URL { get; set; }

    }

    public class GetTiffDataRequestModel
    {
        public string DestPath { get; set; }
        public string SharePath { get; set; }        
    }

    public class PostCommentsRequestModel
    {
        public string ObjId { get; set; }

        public string Comment { get; set; }

        public string AppId { get; set; }

        public string UserId { get; set; }

        public string cmdType { get; set; }
    }

    public class GetRequestData
    {
        public string rqData { get; set; }
    }

    public class GetDocumentResponseModel
    {
        public string ObjId { get; set; }
        public ResponseMessage Response { get; set; }        
        public string FileExtension { get; set; }        
        public byte[] DocumentBinary { get; set; }
        public string DocumentContent { get; set; } 
    }
    public class GetUserWorkListRequestModel
    {
        public string UserId { get; set; }

        public string AppId { get; set; }
        
        public int PageNumber { get; set; }
    }
    public class GetUserWorkListResponseModel
    {
        public List<WISEObject> WorkItems { get; set; }
        public ResponseMessage Response { get; set; }
    }
    
    public class SearchRequestModel
    {
        public string SearchContext { get; set; }

        public List<IndexField> Fields { get; set; }

        public string AppId { get; set; }

        public string UserId { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public int PageNumber { get; set; }
    }

    public class SearchResponseModel
    {
        public List<WISEObject> Results { get; set; }
        public ResponseMessage Response { get; set; }
    }

    public class ConfigURL
    {
        public string Viewer { get; set; }
        public string TiffPath { get; set; }
        public string OutputPath { get; set; }

    }

    public class GetUserAccessInfo
    {
        public string _Status { get; set; } // status of authentication
        public string _FullName { get; set; } // current user full name
        public string _Email { get; set; }   // current user email id        
        public string _AppVersion { get; set; } // App version  whether is custome app or standalone if custome means cApp otherwise sAPP
        public string Config_User { get; set; } // get configured username from web.config - p40awd01
        public string _objID { get; set; } //current objid
        public string _AWDusername { get; set; }
        public string _Password { get; set; }
    }
}