using System;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using TodoApp.Contract;

namespace TodoApp.Api
{
    public class ItemUrlManager:IItemUrlManager
    {
        private readonly UrlHelper _urlHelper;

        public ItemUrlManager(UrlHelper urlHelper)
        {
            urlHelper.Request = (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];
            _urlHelper = urlHelper;
        }

        public string GetItemUrl(Guid id)
        {
            return _urlHelper.Route("DefaultApi", new {id});
        }
    }
}