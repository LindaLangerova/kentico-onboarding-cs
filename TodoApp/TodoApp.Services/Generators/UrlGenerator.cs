using System;
using System.Web.Http.Routing;
using TodoApp.Contract.Services;
using TodoApp.Contract.Services.Generators;
using TodoApp.Contract.Services.Providers;

namespace TodoApp.Services.Generators
{
    internal class UrlGenerator : IUrlGenerator
    {
        private readonly IRouteNameProvider _routeNameProvider;
        private readonly UrlHelper _urlHelper;

        public UrlGenerator(UrlHelper urlHelper, IRouteNameProvider routeNameProvider)
        {
            _urlHelper = urlHelper;
            _routeNameProvider = routeNameProvider;
        }

        public string GetItemUrl(Guid id)
            => _urlHelper.Route(_routeNameProvider.GetRouteName(), new {id});
    }
}
