using System.Web.Http;
using TodoApp.Contract.Repositories;
using TodoApp.Data.Repositories;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace TodoApp.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            container.RegisterType<IItemRepository, ItemRepository>(new HierarchicalLifetimeManager());
        }
    }
}