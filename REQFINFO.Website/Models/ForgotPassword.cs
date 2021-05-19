using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; }



    }
}