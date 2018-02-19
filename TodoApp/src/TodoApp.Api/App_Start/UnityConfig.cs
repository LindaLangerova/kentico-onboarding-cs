using System.Web.Http;
using TodoApp.Data;
using Unity;
using Unity.WebApi;

namespace TodoApp.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            IUnityContainer container = new UnityContainer();

            DataUnityBootstrapper.RegisterComponents(container);
            ApiUnityBootstrapper.RegisterComponents(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}