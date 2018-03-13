using System;
using System.Web.Http;
using TodoApp.Contract;
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

            RegisterTypesBy<DataUnityBootstrapper>(container);
            RegisterTypesBy<ApiUnityBootstrapper>(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterTypesBy<T>(IUnityContainer container) where T : IUnityBootstrapper
        {
            var bootstrapper = Activator.CreateInstance<T>();
            bootstrapper.RegisterTypes(container);
        }
    }
}