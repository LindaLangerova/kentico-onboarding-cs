using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using NUnit.Framework;
using TodoApp.Api.Controllers;
using TodoApp.Api.Models;
using TodoApp.Api.Tests.Comparers;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    public class ItemListControllerTest
    {
        private ItemListController _controller;

        private static async Task<(HttpStatusCode, HttpResponseMessage)> ResolveResult (IHttpActionResult result)
        {
            var message = await result.ExecuteAsync(CancellationToken.None);
            var actualStatusCode = message.StatusCode;
            return (actualStatusCode, message);
        }

        [SetUp]
        public void SetUp()
        {
            _controller = new ItemListController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Test]
        public async Task GetAllItems_AllItemsReturned()
        {
            var expectedItems = new[]
            {
                new ItemModel {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a cofee"},
                new ItemModel {Id = Guid.Parse("55b0d56d-48d7-4f93-bd73-e4b801e26faa"), Text = "Make second coffee"},
                new ItemModel {Id = Guid.Parse("886c2ea5-a639-4334-8c51-d3ee4e49acb9"), Text = "Make third cofffee"},
                new ItemModel {Id = Guid.Parse("250be0cc-438e-46cc-a0fe-549f4d3409e2"), Text = "Coffee is awesome as well as Kentico is"}
            };

            var (actualStatusCode, message) = await ResolveResult(await _controller.GetAllAsync());
            message.TryGetContentValue(out ItemModel[] actualItems);

            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualItems, Is.EqualTo(expectedItems).UsingItemModelComparer());
        }

        [Test]
        public async Task GetItem_ExistingId_ItemReturned()
        {
            var expectedItem = new ItemModel {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a cofee"};

            var (actualStatusCode, message) = await ResolveResult(await _controller.GetAsync(Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3")));
            message.TryGetContentValue(out ItemModel actualItem);

            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualItem, Is.EqualTo(expectedItem).UsingItemModelComparer());
        }

        [Test]
        public async Task PostNewItem_UniqueItem_ItemAdded()
        {
            var expectedItem = new ItemModel { Id = Guid.Parse("a09c0705-b162-4443-b497-9812e6b5c5aa"), Text = "Another coffee" };

            var (actualStatusCode, _) = await ResolveResult(await _controller.PostAsync(expectedItem));

            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
        
        [Test]
        public async Task PutItem_ExistingItem_ItemUpdated()
        {
            var updateItem = new ItemModel { Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Add some milk" };

            var (actualStatusCode, _) = await ResolveResult(await _controller.PutAsync(updateItem.Id, updateItem));
            
            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task PutItem_UnexistingItem_ItemAdded()
        {
            var updateItem = new ItemModel { Id = Guid.Parse("a09c0705-b162-4443-b497-9812e6b5c5aa"), Text = "Coffee overflow" };

            var (actualStatusCode, _) = await ResolveResult(await _controller.PutAsync(updateItem.Id, updateItem));

            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task DeleteItem_ItemDeleted()
        {
            var (actualStatusCode, _) = await ResolveResult(await _controller.DeleteAsync(Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3")));
            
            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

    }
}
