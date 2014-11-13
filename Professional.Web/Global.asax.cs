using Professional.Web.Infrastructure.Mappings;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Professional.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentUICulture;

            ViewEnginesConfig.SetEngines(ViewEngines.Engines);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute();
        }
    }
}
