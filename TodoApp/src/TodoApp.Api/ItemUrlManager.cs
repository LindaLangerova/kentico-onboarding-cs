using System;
using System.Web.Http.Routing;
using TodoApp.Contract;

namespace TodoApp.Api
{
    public class ItemUrlManager:IItemUrlManager
    {
        private readonly UrlHelper _urlHelper;

        public ItemUrlManager(UrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public string GetItemUrl(Guid id)
        {
            return _urlHelper.Route("DefaultApi", new {id});
        }
    }
}