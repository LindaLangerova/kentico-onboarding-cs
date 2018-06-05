using TodoApp.Contract.Services;

namespace TodoApp.Services.Providers
{
    public class RouteNameProvider : IRouteNameProvider
    {
        public string GetRouteName()
            => "DEFAULT_API";
    }
}
