using TodoApp.Contract;
using TodoApp.Contract.Services;
using TodoApp.Services.Creators;
using TodoApp.Services.Generators;
using TodoApp.Services.Providers;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Services
{
    public class ServicesUnityBootstrapper : IUnityBootstrapper
    {
        void IUnityBootstrapper.RegisterTypes(IUnityContainer container)
            => RegisterTypes(container);

        public static void RegisterTypes(IUnityContainer container)
            => container.RegisterType<IUrlGenerator, UrlGenerator>(new ContainerControlledLifetimeManager())
                        .RegisterType<IItemCreator, ItemCreator>(new HierarchicalLifetimeManager())
                        .RegisterType<IItemCacher, ItemCacher>(new HierarchicalLifetimeManager())
                        .RegisterType<IIdGenerator, IdGenerator>(new HierarchicalLifetimeManager())
                        .RegisterType<IConnectionStringProvider, ConnectionStringProvider>(new ContainerControlledLifetimeManager())
                        .RegisterType<IRouteNameProvider, RouteNameProvider>(new HierarchicalLifetimeManager())
                        .RegisterType<IDateTimeGenerator, DateTimeGenerator>(new HierarchicalLifetimeManager());
    }
}
