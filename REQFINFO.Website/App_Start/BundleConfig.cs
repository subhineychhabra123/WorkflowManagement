using System.Web;
using System.Web.Optimization;

namespace REQFINFO.Website
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
           
            /* Business Scripts Bundles - Starts */
            // Set EnableOptimizations to false for debugging. For more information,          
            BundleTable.EnableOptimizations = false;
        }
    }
}
