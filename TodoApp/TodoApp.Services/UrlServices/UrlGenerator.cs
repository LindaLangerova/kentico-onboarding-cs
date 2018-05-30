using System;
using System.Web.Http.Routing;

namespace TodoApp.Services.UrlServices
{
    public class UrlGenerator : IUrlGenerator
    {
        private readonly UrlHelper _urlHelper;

        public UrlGenerator(UrlHelper urlHelper)
            => _urlHelper = urlHelper;

        public string GetItemUrl(Guid id, string routeName)
            => _urlHelper.Route(routeName, new {id});
    }
}
