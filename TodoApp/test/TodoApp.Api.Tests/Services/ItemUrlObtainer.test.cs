using System;
using System.Net.Http;
using System.Web.Http.Routing;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Api.Tests.Utilities;
using TodoApp.Api.Services;

namespace TodoApp.Api.Tests.Services
{
    internal class ItemUrlObtainerTest : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            var request = new HttpRequestMessage
            {
                Version = new Version("2.1")
            };
            var urlHelper = Substitute.For<UrlHelper>(request);

            _itemUrlObtainer = new ItemUrlObtainer(urlHelper);
        }

        private ItemUrlObtainer _itemUrlObtainer;

        [Test]
        public void GetItemUrl_UrlReceived()
        {
            var id = Guid.Parse("5f6a2723-040a-4398-8b63-9d55153378ba");
            var requestedUrl = "api/v2.1/itemlist/5f6a2723-040a-4398-8b63-9d55153378ba";

            var receivedUrl = _itemUrlObtainer.GetItemUrl(id);

            Assert.That(receivedUrl, Is.EqualTo(requestedUrl));
        }
    }
}