using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace TodoApp.Api
{
    public class ItemUrlManager
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