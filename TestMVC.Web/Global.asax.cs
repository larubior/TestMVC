using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestMVC.Web.App_Start;

namespace TestMVC.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // SimpleInjector
            Injector.Configure();
        }
    }
}
