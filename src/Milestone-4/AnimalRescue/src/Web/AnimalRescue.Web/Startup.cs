using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AnimalRescue.Web.Startup))]
namespace AnimalRescue.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
