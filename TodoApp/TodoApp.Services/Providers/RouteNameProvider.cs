using TodoApp.Contract.Services;
using TodoApp.Contract.Services.Providers;

namespace TodoApp.Services.Providers
{
    public class RouteNameProvider : IRouteNameProvider
    {
        public string GetRouteName()
            => "DEFAULT_API";
    }
}
