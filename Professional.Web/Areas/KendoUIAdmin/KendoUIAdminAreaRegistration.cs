using System.Web.Mvc;

namespace Professional.Web.Areas.KendoUIAdmin
{
    public class KendoUIAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "KendoUIAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "KendoUIAdmin_default",
                "KendoUIAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}