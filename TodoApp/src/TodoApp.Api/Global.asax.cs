using System.Web;
using System.Web.Http;

namespace TodoApp.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(RouteConfig.Register);
            GlobalConfiguration.Configure(UnityConfig.Register);
        }
    }
}
