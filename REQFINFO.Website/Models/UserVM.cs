using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class UserVM
    {
        public string IDUser { get; set; }
        public string IDCompany { get; set; }
        public string IDContractor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string PhoneWork { get; set; }
        public string PhoneCell { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }    
}