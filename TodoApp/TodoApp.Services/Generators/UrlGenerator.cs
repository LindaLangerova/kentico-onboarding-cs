﻿using System;
using System.Web.Http.Routing;
using TodoApp.Contract.Services;

namespace TodoApp.Services.Generators
{
    public class UrlGenerator : IUrlGenerator
    {
        private readonly UrlHelper _urlHelper;
        private readonly IRouteNameProvider _routeNameProvider;

        public UrlGenerator(UrlHelper urlHelper, IRouteNameProvider routeNameProvider)
        {
            _urlHelper = urlHelper;
            _routeNameProvider = routeNameProvider;
        }

        public string GetItemUrl(Guid id)
            => _urlHelper.Route(_routeNameProvider.GetRouteName(), new {id});
    }
}
