using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage="Email is required")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage="Email is invalid.")]
        public string EmailAddress { get; set; }

        
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]       
        [Display(Name = "Password")]
        public string Password { get; set; }


    }
}