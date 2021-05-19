using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace REQFINFO.Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
           
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: "ForgotPassword", url: "ForgotPassword", defaults: new { controller = "Site", action = "ForgotPassword" });

            routes.MapRoute(name: "WorkFLow", url: "WorkfLow", defaults: new { controller = "User", action = "WorkFLow" });
            routes.MapRoute(name: "Login", url: "Login", defaults: new { controller = "Site", action = "Login" });
            routes.MapRoute(name: "Projects", url: "Projects/{id}", defaults: new { controller = "User", action = "Projects", id="" });
            routes.MapRoute(name: "GIGCommunicationLog", url: "GIGCommunicationLog/{IDUPW}/{IDProject}", defaults: new { controller = "User", action = "GIGCommunicationLog",IDUPW = "",IDProject = "" });
            routes.MapRoute(name: "CreateGIG", url: "CreateGIG/{IDUPW}/{IDProject}/{IDLevelXFunction}", defaults: new { controller = "User", action = "CreateGIG", IDUPW = "", IDProject = "", IDLevelXFunction="", });


            routes.MapRoute(name: "ProccessGIG", url: "ProccessGIG/{IDGIG}/{IDLevelXFunction}/{IDFunctionTrigger}", defaults: new { controller = "User", action = "ProccessGIG", IDGIG = "", IDLevelXFunction = UrlParameter.Optional, IDFunctionTrigger =UrlParameter.Optional });
           

            
            routes.MapRoute(name: "Default", url: "{controller}/{action}/", defaults: new { controller = "Site", action = "Login" });
            
        }
    }
}
