using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using REQFINFO.Website.Infrastructure;
using System.Web.Optimization;
using REQFINFO.Business.Infrastructure;

namespace REQFINFO.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            StructureMapper.Run();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ExpressMapperWebProfile.RegisterMapping();
            ExpressMapperBusinessProfile.RegisterMapping();
        }
        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session != null)
            {
                if (REQFINFO.Utility.SessionManagement.LoggedInUser == null || REQFINFO.Utility.SessionManagement.LoggedInUser.IDUser == 0)
                {
                    string[] groups = null;
                    System.Security.Principal.GenericIdentity id = new System.Security.Principal.GenericIdentity(string.Empty, "RIAuthentication");
                    System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(id, groups);
                    Context.User = principal;
                }
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // code to change culture
        }

        protected void Application_AuthenticateRequest(object sender, System.EventArgs e)
        {
            string cookieName = System.Web.Security.FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = Context.Request.Cookies[cookieName];

            if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
            {
                System.Web.Security.FormsAuthenticationTicket authTicket = null;
                authTicket = System.Web.Security.FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null)
                {
                    string[] groups = authTicket.UserData.Split('|');
                    System.Security.Principal.GenericIdentity id = new System.Security.Principal.GenericIdentity(authTicket.Name, "RIAuthentication");
                    System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(id, groups);
                    Context.User = principal;

                }
            }
        }
    }
}
