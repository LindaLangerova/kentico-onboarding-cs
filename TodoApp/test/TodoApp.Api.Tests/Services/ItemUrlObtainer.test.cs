using System;
using System.Net.Http;
using System.Web.Http.Routing;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Api.Services;
using TodoApp.Api.Tests.Utilities;

namespace TodoApp.Api.Tests.Services
{
    internal class ItemUrlGeneratorTest : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            var request = new HttpRequestMessage
                {Version = new Version("2.1")};

            var urlHelper = Substitute.For<UrlHelper>(request);

            urlHelper.Route("DefaultApi", Arg.Any<object>())
                     .Returns("api/v2.1/itemlist/5f6a2723-040a-4398-8b63-9d55153378ba");

            _itemUrlGenerator = new UrlGenerator(urlHelper);
        }

        private UrlGenerator _itemUrlGenerator;

        [Test]
        public void GetItemUrl_UrlReceived()
        {
            var id = Guid.Parse("5f6a2723-040a-4398-8b63-9d55153378ba");
            var requestedUrl = "api/v2.1/itemlist/5f6a2723-040a-4398-8b63-9d55153378ba";

            var receivedUrl = _itemUrlGenerator.GetItemUrl(id);

            Assert.That(receivedUrl, Is.EqualTo(requestedUrl));
        }
    }
}
