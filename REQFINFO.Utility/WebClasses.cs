using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Utility
{
    [Serializable]
    public class CurrentUser
    {
        public int IDUser { get; set; }
        public string UserIdEncrypted { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int IDCompany { get; set; }
        public int IDProject { get; set; }

      

       
        
        
        private string _profileImageUrl = string.Empty;
        public string ProfileImageUrl
        {
            get
            {
                return string.IsNullOrWhiteSpace(this._profileImageUrl) ? "no_image.gif" : this._profileImageUrl;
            }
            set { this._profileImageUrl = value; }
        }
        //public string UserType
        //{
        //    get
        //    {
        //        return UserTypeId.ToString();
        //    }
        //}
       
        private string _companyName;
        public string CompanyName 
        {
            set
            {
                _companyName = value;
            }
            get
            {
                if(!String.IsNullOrEmpty(_companyName))
                {
                    if (_companyName.Length > 30)
                        _companyName= _companyName.Substring(0, 25) + ". . .";
                    
                }
                return _companyName;
            }
        }

    }
    public class MailBodyTemplate
    {
        public string RegistrationUserName { get; set; }
        public string AccountLoginUserId { get; set; }
        public string AccountLoginPassowrd { get; set; }
        public string AccountLoginUrl { get; set; }
        public string MailBody { get; set; }
        public string WebSiteLogoPath { get; set; }
        public string AccessForDeploymentModule { get; set; }
        public string ModulesAccessType { get; set; }
        public string ModuleAccessValidFrom { get; set; }
        public string ModuleAccessValidTill { get; set; }
        public string Comment_By_Admin { get; set; }
        public string Admin_Message_toUser { get; set; }

        public string PayerFullName { get; set; }


    }
    public class Response
    {
        public Response()
        {
            this.StatusCode = 200;

        }
        public string Status;
        public int StatusCode;
        public string Message;
    }
  
   
}
