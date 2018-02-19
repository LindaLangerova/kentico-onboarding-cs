using Unity;

namespace TodoApp.Contract
{
    public interface IUnityBootstrapper
    {
        void RegisterComponents(IUnityContainer container);
    }
}
