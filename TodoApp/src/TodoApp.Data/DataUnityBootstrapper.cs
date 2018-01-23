using TodoApp.Contract;
using TodoApp.Contract.Repositories;
using TodoApp.Data.Repositories;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Data
{
    public class DataUnityBootstrapper : IUnityBootstrapper
    {
        void IUnityBootstrapper.RegisterTypes(IUnityContainer container)
            => RegisterTypes(container);

        public void RegisterTypes(IUnityContainer container)
            => container
                .RegisterType<IItemRepository, ItemRepository>(new HierarchicalLifetimeManager());
    }
}
