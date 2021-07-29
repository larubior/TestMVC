using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Configuration;
using System.Reflection;
using System.Web.Mvc;
using TestMVC.Business.Interfaces;
using TestMVC.Business.Operations;
using TestMVC.Data.Interfaces;
using TestMVC.Data.Operations;

namespace TestMVC.Web.App_Start
{
    /// <summary>
    /// Summary description for Injector
    /// </summary>
    public class Injector
    {
        public static void Configure()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register your types, for instance:
            //Repositorys
            container.Register<IUserRepository>(
                () => new UserRepository(ConfigurationManager.ConnectionStrings["TestMVC"].ConnectionString),
                Lifestyle.Singleton);

            // Business
            container.Register<IUserBusiness, UserBusiness>(Lifestyle.Transient);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}