using System.Net.Http;
using System.Web.Http.Routing;
using TodoApp.Contract;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace TodoApp.Api
{
    public class ApiUnityBootstrapper: IUnityBootstrapper
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            container
                .RegisterType<IItemUrlManager, ItemUrlManager>(new HierarchicalLifetimeManager())
                .RegisterType<UrlHelper, UrlHelper>(new HierarchicalLifetimeManager())
                .RegisterType<HttpRequestMessage, HttpRequestMessage>(new InjectionConstructor());
        }

        void IUnityBootstrapper.RegisterComponents(IUnityContainer container)
        {
            RegisterComponents(container);
        }
    }
}