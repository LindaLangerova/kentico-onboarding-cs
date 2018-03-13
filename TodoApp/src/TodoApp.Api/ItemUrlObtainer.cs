using System;
using System.Web.Http.Routing;
using TodoApp.Contract;

namespace TodoApp.Api
{
    public class ItemUrlObtainer : IItemUrlObtainer
    {
        private readonly UrlHelper _urlHelper;

        public ItemUrlObtainer(UrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public string GetItemUrl(Guid id) => $"api/v{_urlHelper.Request.Version}/itemlist/{id}";
    }
}