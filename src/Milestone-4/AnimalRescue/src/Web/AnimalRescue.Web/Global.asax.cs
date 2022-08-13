using AnimalRescue.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AnimalRescue
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        { 
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.RegisterComponents();
        }
    }
}
