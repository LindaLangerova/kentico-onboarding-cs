using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace TodoApp.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(RouteConfig.Register);

            UnityConfig.RegisterComponents();
        }
    }
}
