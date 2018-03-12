using MongoDB.Driver.Core.Configuration;
using TodoApp.Contract;
using TodoApp.Contract.Repositories;
using TodoApp.Data.Contexts;
using TodoApp.Data.Repositories;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace TodoApp.Data
{
    public class DataUnityBootstrapper: IUnityBootstrapper
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            container
                .RegisterType<IItemRepository, ItemRepository>(new HierarchicalLifetimeManager())
                .RegisterType<MongoDbContext, MongoDbContext>(new HierarchicalLifetimeManager(),
                    new InjectionConstructor(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));
        }

        void IUnityBootstrapper.RegisterComponents(IUnityContainer container)
        {
            RegisterComponents(container);
        }
    }
}