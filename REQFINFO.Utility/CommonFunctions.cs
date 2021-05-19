using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Xml;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Drawing;

namespace REQFINFO.Utility
{
    public class CommonFunctions
    {
        public static void SetCookie(CurrentUser loggedInUser, string pipeSeperatedPermissions, bool rememberMe)
        {            
            FormsAuthentication.SetAuthCookie(loggedInUser.Email, false);
            FormsAuthenticationTicket ticket1 =
               new FormsAuthenticationTicket(
                    1,                                   // version
                    loggedInUser.IDUser.ToString(),   // get username  from the form
                    DateTime.Now,                        // issue time is now
                    DateTime.Now.AddMinutes(2080),
                    false,      // cookie is not persistent
                 "Test"// loggedInUser.RoleName + "|" + pipeSeperatedPermissions// role assignment is stored
                // in userData
                    );
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
            HttpContext.Current.Response.Cookies.Add(cookie);
            SessionManagement.LoggedInUser = loggedInUser;
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("userid", loggedInUser.IDUser.Encrypt()));
            HttpCookie permissionCookie = HttpContext.Current.Response.Cookies["Permissions"];


            if (permissionCookie != null)
                permissionCookie = new HttpCookie("Permissions", "");
            permissionCookie = new HttpCookie("Permissions", pipeSeperatedPermissions);
            //permissionCookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Current.Response.Cookies.Add(permissionCookie);

            HttpCookie rememberCookie = new HttpCookie("RememberMe");
            if (rememberMe)
            {
                rememberCookie.Values["password"] = loggedInUser.Password;               
                rememberCookie.Values["emailaddress"] = loggedInUser.Email;
                rememberCookie.Expires = DateTime.Now.AddYears(1);
            }
            else
            {
                rememberCookie.Expires = DateTime.Now.AddDays(-1);
            }
            HttpContext.Current.Response.Cookies.Add(rememberCookie);
        }
        public static string ReadFile(string filePath)
        {
            string text = string.Empty;

            if (File.Exists(filePath))
            {
                System.IO.StreamReader myFile = new System.IO.StreamReader(filePath);
                text = myFile.ReadToEnd();
                myFile.Close();
            }
            return text;
        }
        public static bool CreateFolder(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }
        public static string GeneratePassword()
        {
            string allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ#@$&+";
            string newPassword = null;
            Random randNum = new Random();
            char[] chars = new char[8];   // Password length is 8
            int allowedCharCount = allowedChars.Length; // Total length of allowed characters
            for (int i = 0; i < 8; i++)
            {
                chars[i] = allowedChars[(int)((allowedCharCount) * randNum.NextDouble())];
                newPassword += chars[i];
            }
            return newPassword;
        }
        public static string ConfigurePasswordRecoveryMailBody(MailBodyTemplate mailBodyTemplate)
        {
            string messageBody = mailBodyTemplate.MailBody;
            messageBody = messageBody.Replace(Constants.AccountLoginUserName, mailBodyTemplate.RegistrationUserName)
            .Replace(Constants.AccountLoginUserId, mailBodyTemplate.AccountLoginUserId)
            .Replace(Constants.AccountLoginPassowrd, mailBodyTemplate.AccountLoginPassowrd);
            
            return messageBody;
        }

      
        public static string FormateDate(DateTime dateToFormat){
            return dateToFormat.ToString("MM/dd/yyyy");
        }
        public static DateTime ConvertToDate(string dateToFormat)
        {
            return Convert.ToDateTime(dateToFormat);
        }
        public static string ConcatenateStrings(string firstString, string secondString = "")
        {
            string concatenatedString = string.Empty;
            if (!string.IsNullOrEmpty(firstString) && !string.IsNullOrEmpty(secondString))
                concatenatedString = firstString + " " + secondString;
            else if (!string.IsNullOrEmpty(firstString))
                concatenatedString = firstString;
            else if (!string.IsNullOrEmpty(secondString))
                concatenatedString = secondString;

            return concatenatedString;
        }

        public static void UploadFile(HttpPostedFileBase file, string fileSavingPath, int imageWidth = 0, int imageHeight = 0)
        {

            //if (!Directory.Exists(imageSavingPath))
            //    Directory.CreateDirectory(imageSavingPath);
            if (System.IO.File.Exists(fileSavingPath))
            {
                System.IO.File.Delete(fileSavingPath);
            }
            file.SaveAs(fileSavingPath);
            if (imageWidth > 0 && imageHeight > 0)
            {
                var resizeSettings = new ImageResizer.ResizeSettings
                {
                    Scale = ImageResizer.ScaleMode.DownscaleOnly,
                    Width = imageWidth,
                    Height = imageHeight,
                    Mode = ImageResizer.FitMode.Crop
                };
                var b = ImageResizer.ImageBuilder.Current.Build(fileSavingPath, resizeSettings);
                b.Save(fileSavingPath);
            }

        }
    }

}
