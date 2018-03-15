using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using TodoApp.Api.Services;
using TodoApp.Contract;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace TodoApp.Api
{
    public class ApiUnityBootstrapper : IUnityBootstrapper
    {
        void IUnityBootstrapper.RegisterTypes(IUnityContainer container)
        {
            RegisterTypes(container);
        }

        public void RegisterTypes(IUnityContainer container) => container
                .RegisterType<IItemUrlObtainer, ItemUrlObtainer>(new HierarchicalLifetimeManager())
                .RegisterType<UrlHelper, UrlHelper>(new HierarchicalLifetimeManager())
                .RegisterType<HttpRequestMessage, HttpRequestMessage>(new InjectionConstructor());
    }
}