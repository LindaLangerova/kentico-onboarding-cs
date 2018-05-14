using System.Net.Http;
using System.Web;
using TodoApp.Api.Services;
using TodoApp.Contract;
using TodoApp.Contract.Services;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace TodoApp.Api
{
    public class ApiUnityBootstrapper : IUnityBootstrapper
    {
        void IUnityBootstrapper.RegisterTypes(IUnityContainer container)
            => RegisterTypes(container);

        public void RegisterTypes(IUnityContainer container)
            => container.RegisterType<IUrlGenerator, UrlGenerator>(new HierarchicalLifetimeManager())
                        .RegisterType<HttpRequestMessage, HttpRequestMessage>(
                            new HierarchicalLifetimeManager(), new InjectionFactory(GetActualRequestMessage));

        private static HttpRequestMessage GetActualRequestMessage(IUnityContainer container)
            => HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
    }
}
