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
    public class UrlGeneratorTest : TestBase
    {
        private UrlGenerator _itemUrlGenerator;

        private static string AnonymousArg(object first, string argName)
        {
            new RouteValueDictionary(first).TryGetValue(argName, out var firstId);
            return firstId?.ToString();
        }

        [Test]
        public void GetItemUrl_UrlReceived()
        {
            var urlHelper = Substitute.For<UrlHelper>();
            _itemUrlGenerator = new UrlGenerator(urlHelper);

            urlHelper.Route(RouteConfig.DefaultApi,
                            Arg.Is<object>(o => AnonymousArg(o, "id").Equals("5f6a2723-040a-4398-8b63-9d55153378ba")))
                     .Returns("api/5f6a2723-040a-4398-8b63-9d55153378ba/v2.1/itemlist");

            var id = Guid.Parse("5f6a2723-040a-4398-8b63-9d55153378ba");

            var requestedUrl = "api/5f6a2723-040a-4398-8b63-9d55153378ba/v2.1/itemlist";

            var receivedUrl = _itemUrlGenerator.GetItemUrl(id, RouteConfig.DefaultApi);

            Assert.That(receivedUrl, Is.EqualTo(requestedUrl));
        }
    }
}
