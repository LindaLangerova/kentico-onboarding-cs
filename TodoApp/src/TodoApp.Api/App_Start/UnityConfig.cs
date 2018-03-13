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

            var dataUnityBootstrapper = new DataUnityBootstrapper();
            dataUnityBootstrapper.RegisterTypes(container);

            var apiUnityBootstrapper = new ApiUnityBootstrapper();
            apiUnityBootstrapper.RegisterTypes(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}