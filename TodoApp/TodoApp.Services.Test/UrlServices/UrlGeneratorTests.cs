using System;
using System.Web.Http.Routing;
using System.Web.Routing;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Api;
using TodoApp.Contract.Tests.Utilities;
using TodoApp.Services.Generators;

namespace TodoApp.Services.Test.UrlServices
{
    public class UrlGeneratorTests : TestBase
    {
        private UrlGenerator _itemUrlGenerator;
        private static readonly Guid FakeId = Guid.Parse("5f6a2723-040a-4398-8b63-9d55153378ba");

        [Test]
        public void GetItemUrl_UrlReceived()
        {
            var urlHelper = Substitute.For<UrlHelper>();
            _itemUrlGenerator = new UrlGenerator(urlHelper);

            urlHelper.Route(RouteConfig.DefaultApi, Arg.Is<object>(o => ContainsCorrectId(o))).Returns($"api/{FakeId}/v2.1/itemlist");

            var id = FakeId;

            var requestedUrl = $"api/{FakeId}/v2.1/itemlist";

            var receivedUrl = _itemUrlGenerator.GetItemUrl(id, RouteConfig.DefaultApi);

            Assert.That(receivedUrl, Is.EqualTo(requestedUrl));
        }

        private static bool ContainsCorrectId(object resolvedObject)
            => new RouteValueDictionary(resolvedObject).TryGetValue("id", out var obtainedId) && obtainedId.Equals(FakeId);
    }
}
