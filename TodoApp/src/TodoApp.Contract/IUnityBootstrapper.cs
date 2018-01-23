using Unity;

namespace TodoApp.Contract
{
    public interface IUnityBootstrapper
    {
        void RegisterTypes(IUnityContainer container);
    }
}
