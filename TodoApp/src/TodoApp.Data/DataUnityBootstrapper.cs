using TodoApp.Contract;
using TodoApp.Contract.Repositories;
using TodoApp.Data.Repositories;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Data
{
    public class DataUnityBootstrapper: IUnityBootstrapper
    {
        public void RegisterComponents(IUnityContainer container)
        {
            container
                .RegisterType<IItemRepository, ItemRepository>(new HierarchicalLifetimeManager());
        }

        void IUnityBootstrapper.RegisterComponents(IUnityContainer container)
        {
            RegisterComponents(container);
        }
    }
}