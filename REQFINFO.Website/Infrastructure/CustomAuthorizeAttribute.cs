using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace REQFINFO.Website.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private bool _isAuthorized;
        public int UserRole { set; get; }
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            _isAuthorized = base.AuthorizeCore(httpContext);
            return _isAuthorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    // filterContext.Result = new RedirectToRouteResult(new
                    //RouteValueDictionary(new { controller = "Error", action = "SessionTimeOut" }));
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.Result = new JsonResult { Data = "LogOut", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    //if(UserRole==(int)REQFINFO.Utility.Enums.Role.User)
                    //{
                    //    filterContext.Result = new RedirectToRouteResult(new
                    //    RouteValueDictionary(new { controller = "Site", action = "Index" }));
                    //}
                    //if (UserRole == (int)REQFINFO.Utility.Enums.Role.Admin)
                    //{
                    //    filterContext.Result = new RedirectToRouteResult(new
                    //    RouteValueDictionary(new { controller = "Site", action = "AdminLogin" }));
                    //}
                }
            }
            //else if (!_isAuthorized)
            //{
            //    filterContext.Result = new RedirectToRouteResult(new
            //    RouteValueDictionary(new { controller = "Error", action = "Index" }));
            //}
        }
    }
    public class FileTypesAttribute : ValidationAttribute
    {
        private readonly List<string> _types;

        public FileTypesAttribute(string types)
        {
            _types = types.Split(',').ToList();
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            var fileExt = System.IO.Path.GetExtension((value as HttpPostedFileBase).FileName).Substring(1);
            return _types.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Invalid file type. Only the following types {0} are supported.", String.Join(", ", _types));
        }
    }
}