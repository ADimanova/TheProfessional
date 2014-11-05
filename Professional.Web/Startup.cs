using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Professional.Web.Startup))]
namespace Professional.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
