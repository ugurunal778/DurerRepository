using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace durer2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string cookieVal = "";
            var cookie = Request.Cookies["CultureInfo"];
            if (cookie == null)
            {
                cookieVal = "en-US";
            }
            else
            {
                cookieVal = cookie.Value;
            }
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookieVal);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cookieVal);
        }
    }
}
