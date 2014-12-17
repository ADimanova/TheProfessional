using Newtonsoft.Json;
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
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEnginesConfig.SetEngines(ViewEngines.Engines);
            AutoMapperConfig.Execute();
            

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            JsonSerializer.CreateDefault(jsonSerializerSettings);

            //GlobalConfiguration.Configuration.Formatters.Clear();
            //GlobalConfiguration.Configuration.Formatters.Add(new JsonNetFormatter(jsonSerializerSettings));
        }
    }
}
