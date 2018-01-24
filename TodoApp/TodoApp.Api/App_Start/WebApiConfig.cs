﻿using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Web.Http.Routing;
using TodoApp.Data.Repositories;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Api
{
    public static class RouteConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            var versionConstraintResolver = new DefaultInlineConstraintResolver
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof( ApiVersionRouteConstraint )
                }
            };
            config.MapHttpAttributeRoutes(versionConstraintResolver);
        }
    }
}
