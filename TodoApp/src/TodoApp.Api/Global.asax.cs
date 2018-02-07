using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace TodoApp.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IUnityContainer container = new UnityContainer();

            GlobalConfiguration.Configure(RouteConfig.Register);

            UnityConfig.RegisterComponents(container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
