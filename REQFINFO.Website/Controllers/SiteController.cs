using REQFINFO.Business;
using REQFINFO.Domain;
using REQFINFO.Business.Interfaces;
using REQFINFO.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REQFINFO.Utility;

namespace REQFINFO.Website.Controllers
{
    public class SiteController : Controller
    {
        private IUserBusiness userBusiness;
        public SiteController(IUserBusiness _userBusiness)
        {
            userBusiness = _userBusiness;
        }
        //
        // GET: /Site/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Site/login
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        [AllowAnonymous]
        public ActionResult Login(LoginVM LoginModel)
        {
            UserModel userModel = userBusiness.ValidateUser(LoginModel.EmailAddress, LoginModel.Password);
            if (userModel.Email != null)
            {
                SessionManagement.LoggedInUser.IDUser = userModel.IDUser;
                SessionManagement.LoggedInUser.IDCompany = userModel.IDCompany;
                SessionManagement.LoggedInUser.FullName = userModel.UserName + " " + userModel.LastName;

                return RedirectToRoute("WorkfLow");

            }
            else
            {
                ViewBag.ValidationMsg = "Invalid Email or Password";
                return View("login");
            }
        }

        /// <summary>
        /// Forgot password
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword ForgotPassword)
        {
            UserModel UserModel = userBusiness.ForgotPassword(ForgotPassword.Email);
            if (UserModel.IDUser != 0)
            {
                var MailResponce = SetPasswordByAdmin(UserModel);
                if (MailResponce.StatusCode != 200)
                {
                    // ViewBag.ValidationMsg = MailResponce.Message;
                    TempData["ValidationMsg"] = MailResponce.Message;
                }
                else
                {
                    // ViewBag.SuccessMsg = MailResponce.Message;
                    TempData["SuccessMsg"] = MailResponce.Message;

                }
            }
            else
            {

                TempData["ValidationMsg"] = "The email '" + ForgotPassword.Email + "' is not registered for this site. Please try again.";
            }


            return RedirectToAction("ForgotPassword");
        }



        public Response SetPasswordByAdmin(UserModel UserModel)
        {

            bool isMailSent = true;
            Response response = new Response();
            MailHelper mailHelper = new MailHelper();
            MailBodyTemplate mailBodyTemplate = new MailBodyTemplate();
            string mailTemplateFolder = ReadConfiguration.MailTemplateFolder;
            string mailBody = string.Empty;
            mailHelper.Subject = Constants.PasswordRecoveryMailSubject;
            mailHelper.ToEmail = UserModel.Email;
            mailBody = CommonFunctions.ReadFile(Server.MapPath(mailTemplateFolder + Constants.PasswordRecoveryMailFileName));
            mailBodyTemplate.RegistrationUserName = UserModel.FirstName;
            mailBodyTemplate.MailBody = mailBody;
            mailBodyTemplate.AccountLoginUserId = UserModel.Email;
            mailBodyTemplate.AccountLoginPassowrd = UserModel.Password;
            mailBodyTemplate.AccountLoginUrl = ReadConfiguration.WebsiteLogoPath;
            mailBody = CommonFunctions.ConfigurePasswordRecoveryMailBody(mailBodyTemplate);
            mailHelper.Body = mailBody;
            isMailSent = mailHelper.SendEmail();
            //TODO: //! use enum to set status rather than string value
            //TODO: //! handle the messages at client end "Message.js" file
            if (isMailSent)
            {

                response.Message = "An email has been sent to you with existing password. ";

                response.Status = "Success";


            }
            else
            {
                response.Message = "Error while sending mail to user. Please try again.";
                response.Status = "Fail";
            }


            return response;
        }

    }
}
