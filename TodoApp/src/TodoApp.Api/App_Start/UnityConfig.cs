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
        public static void Register(HttpConfiguration config)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterTypesBy<DataUnityBootstrapper>();
            container.RegisterTypesBy<ApiUnityBootstrapper>();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterTypesBy<T>(this IUnityContainer container) where T : IUnityBootstrapper
        {
            var bootstrapper = Activator.CreateInstance<T>();
            bootstrapper.RegisterTypes(container);
        }
    }
}