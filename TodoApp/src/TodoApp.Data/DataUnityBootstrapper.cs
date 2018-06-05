using TodoApp.Contract;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services;
using TodoApp.Data.Repositories;
using TodoApp.Services.Providers;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Data
{
    public class DataUnityBootstrapper : IUnityBootstrapper
    {
        void IUnityBootstrapper.RegisterTypes(IUnityContainer container)
            => RegisterTypes(container);

        public static void RegisterTypes(IUnityContainer container)
            => container.RegisterType<IItemRepository, ItemRepository>(new ContainerControlledLifetimeManager());
    }
}
