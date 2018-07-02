using System.Net.Http;
using System.Web;
using TodoApp.Contract;
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
            => container.RegisterType<HttpRequestMessage, HttpRequestMessage>(new HierarchicalLifetimeManager(),
                                                                              new InjectionFactory(GetActualRequestMessage));

        private static HttpRequestMessage GetActualRequestMessage(IUnityContainer container)
            => HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
    }
}
