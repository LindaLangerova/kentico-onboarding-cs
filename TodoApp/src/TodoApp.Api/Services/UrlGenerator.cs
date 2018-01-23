using System;
using System.Web.Http.Routing;
using TodoApp.Contract.Services;

namespace TodoApp.Api.Services
{
    public class UrlGenerator : IUrlGenerator
    {
        private readonly UrlHelper _urlHelper;

        public UrlGenerator(UrlHelper urlHelper)
            => _urlHelper = urlHelper;

        public string GetItemUrl(Guid id)
            => _urlHelper.Route("DefaultApi", new
                                    {id});
    }
}
