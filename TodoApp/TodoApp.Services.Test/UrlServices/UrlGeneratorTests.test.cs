using System;
using System.Web.Http.Routing;
using System.Web.Routing;
using NSubstitute;
using NUnit.Framework;
using Todo.App.Services.UrlServices;
using TodoApp.Api;
using TodoApp.Contract.Tests.Utilities;

namespace TodoApp.Services.Test.UrlServices
{
    public class UrlGeneratorTests : TestBase
    {
        private UrlGenerator _itemUrlGenerator;

        [Test]
        public void GetItemUrl_UrlReceived()
        {
            var urlHelper = Substitute.For<UrlHelper>();
            _itemUrlGenerator = new UrlGenerator(urlHelper);

            urlHelper.Route(RouteConfig.DefaultApi, Arg.Is<object>(o => ContainsCorrectId(o)))
                     .Returns("api/5f6a2723-040a-4398-8b63-9d55153378ba/v2.1/itemlist");

            var id = Guid.Parse("5f6a2723-040a-4398-8b63-9d55153378ba");

            var requestedUrl = "api/5f6a2723-040a-4398-8b63-9d55153378ba/v2.1/itemlist";

            var receivedUrl = _itemUrlGenerator.GetItemUrl(id, RouteConfig.DefaultApi);

            Assert.That(receivedUrl, Is.EqualTo(requestedUrl));
        }

        private static bool ContainsCorrectId(object first)
        {
            new RouteValueDictionary(first).TryGetValue("id", out var firstId);

            return firstId != null && firstId.Equals(Guid.Parse("5f6a2723-040a-4398-8b63-9d55153378ba"));
        }
    }
}
