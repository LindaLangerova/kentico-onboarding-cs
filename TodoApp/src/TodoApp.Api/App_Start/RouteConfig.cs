using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Web.Http.Routing;

namespace TodoApp.Api
{
    public static class RouteConfig
    {
        public const string DefaultApi = "DEFAULT_API";

        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            // Web API routes
            var versionConstraintResolver = new DefaultInlineConstraintResolver
            {
                ConstraintMap = {["apiVersion"] = typeof(ApiVersionRouteConstraint)}
            };

            config.MapHttpAttributeRoutes(versionConstraintResolver);

            config.Routes.MapHttpRoute(DefaultApi, "api/v{version:apiVersion}/{controller}/{id}",
                                       new {id = RouteParameter.Optional});
        }
    }
}
