using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;
using System.Drawing;
using System.IO;
using System.Web.Mvc;

namespace REQFINFO.Utility
{
    public static class ExtensionMethods
    {
        public static string Encrypt(this Int32 toEncrypt)
        {
            if (toEncrypt == 0)
                return "0";

            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(SessionManagement.LoggedInUser.IDUser + "-" + toEncrypt.ToString());

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = "App";
            // (string)settingsReader.GetValue("SecurityKey",
            //                                 typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            ////If hashing use get hashcode regards to your key
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //Always release the resources and flush data
            // of the Cryptographic service provide. Best Practice

            //hashmd5.Clear();
            //else
            //keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format

            return Convert.ToBase64String(resultArray, 0, resultArray.Length).Replace("/", "^").Replace("+", "~");

        }
        public static string Encrypt(this Int32? toEncrypt)
        {
            if (toEncrypt == null)
                return string.Empty;
            int valueToEncrypt = Convert.ToInt32(toEncrypt);
            return valueToEncrypt.Encrypt();
        }
        public static int Decrypt(this string cipherString)
        {
            int checkInt = 0;
            if (string.IsNullOrEmpty(cipherString) || (int.TryParse(cipherString, out checkInt) && checkInt == 0))
                return 0;

            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString.Replace("^", "/").Replace("~", "+"));

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = "App";
            //(string)settingsReader.GetValue("SecurityKey",
            //                                       typeof(String));

            //if hashing was used get the hash code with regards to your key
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            ////release any resource held by the MD5CryptoServiceProvider

            //hashmd5.Clear();

            //else
            //{
            //if hashing was not implemented get the byte code of the key
            // keyArray = UTF8Encoding.UTF8.GetBytes(key);
            //}

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            string result = UTF8Encoding.UTF8.GetString(resultArray);
            try
            {
                int iresult = 0;
                if (Convert.ToInt32(result.Split('-')[0]) == SessionManagement.LoggedInUser.IDUser && int.TryParse(result.Split('-')[1], out iresult))
                    return iresult;
            }
            catch (Exception ex)
            {
                return -1;
            }
            return -1;
        }
        public static int? Decrypt(this string cipherString, bool returnNullIfZeroOrEmpty)
        {
            int checkInt = 0;
            int? returnDecryptValue;
            if (string.IsNullOrEmpty(cipherString) || (int.TryParse(cipherString, out checkInt) && checkInt == 0))
                if (returnNullIfZeroOrEmpty)
                    return null;
                else returnDecryptValue = 0;
            else
                returnDecryptValue = cipherString.Decrypt();
            return returnDecryptValue;
        }

        //public static DateTime ToDateTimeNow(this DateTime utcDate)
        //{
        //    string offset = SessionManagement.LoggedInUser.TimeZoneOffSet;
        //    DateTime dateObj = new DateTime();
        //    dateObj = utcDate.AddHours(Convert.ToDouble(offset));
        //    return dateObj;
        //}
        public static bool IsValidEmailAddress(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;
            else
            {
                var regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                return regex.IsMatch(s) && !s.EndsWith(".");
            }
        }
        public static String DateStringFormatter(this DateTime? dateToFormat)
        {
            return dateToFormat == null ? String.Empty : CommonFunctions.FormateDate(Convert.ToDateTime(dateToFormat));
        }
        public static String DateStringFormatter(this DateTime dateToFormat)
        {
            return CommonFunctions.FormateDate(dateToFormat);
        }
        public static DateTime? StringDateFormatter(this string dateToFormat)
        {
            DateTime? dt = null;
            if (!string.IsNullOrEmpty(dateToFormat)) { dt = CommonFunctions.ConvertToDate(dateToFormat); }
            return dt;
        }
        public static Image GetImageFromBytes(byte[] imageBytes)
        {
            var stream = new MemoryStream(imageBytes);
            return Bitmap.FromStream(stream);
        }
        /// <summary>
        ///  Gives actual name of Resume File, This methods removes Guid Attathed to resume file at the time of saving.
        /// </summary>
        public static string GetActualResumeName(this string resumeName, int userId)
        {
            string actualResume = null;
            if (resumeName != null)
            {
                if(resumeName.IndexOf("_" + userId)>=0)
                {
                    string filteredString = Path.GetFileNameWithoutExtension(resumeName).Substring(0, Path.GetFileNameWithoutExtension(resumeName).IndexOf("_" + userId));
                    actualResume = filteredString + Path.GetExtension(resumeName);
                }
            }
            return actualResume;
        }
      
        public static MvcHtmlString Nl2Br(this HtmlHelper htmlHelper, string text)
        {
            if (string.IsNullOrEmpty(text))
                return MvcHtmlString.Create(text);
            else
            {
                StringBuilder builder = new StringBuilder();
                string[] lines = text.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i > 0)
                        builder.Append("<br/>\n");
                    builder.Append(HttpUtility.HtmlEncode(lines[i]));
                }
                return MvcHtmlString.Create(builder.ToString());
            }
        }



    }
}
