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
    internal static class ControllerExtensions
    {
        private static async Task<HttpStatusCode> ResolveResult(IHttpActionResult result)
        {
            var message = await result.ExecuteAsync(CancellationToken.None);
            var actualStatusCode = message.StatusCode;
            return actualStatusCode;
        }

        public static async Task<(HttpStatusCode, TResult)> ResolveResult<TController,TResult>(this TController controller, Func<TController,Task<IHttpActionResult>> actionSelector)
        {
            var result = await actionSelector(controller);
            var message = await result.ExecuteAsync(CancellationToken.None);
            var actualStatusCode = await ResolveResult(result);
            message.TryGetContentValue(out TResult actualItems);
            return (actualStatusCode, actualItems);
        }

        public static async Task<HttpStatusCode> ResolveResult<TController>(this TController controller, Func<TController, Task<IHttpActionResult>> actionSelector)
        {
            var result = await actionSelector(controller);
            var actualStatusCode = await ResolveResult(result);
            return actualStatusCode;
        }
    }

    [TestFixture]
    public class ItemListControllerTest
    {
        private ItemListController _controller;
        
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
                new ItemModel {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d2"), Text = "Add some coffee"},
                new ItemModel {Id = Guid.Parse("250be0cc-438e-46cc-a0fe-549f4d3409e2"), Text = "Coffee overflow"}
            };

            var (actualStatusCode, actualItems) =
                await _controller.ResolveResult<ItemListController, ItemModel[]>(controller => controller.GetAllAsync());

            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualItems, Is.EqualTo(expectedItems).UsingItemModelComparer());
        }

        [Test]
        public async Task GetItem_ExistingId_ItemReturned()
        {
            var expectedItem = new ItemModel {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a cofee"};
            var guid = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3");

            var (actualStatusCode, actualItem) = await _controller.ResolveResult<ItemListController, ItemModel>(controller => controller.GetAsync(guid));

            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualItem, Is.EqualTo(expectedItem).UsingItemModelComparer());
        }

        [Test]
        public async Task PostNewItem_UniqueItem_ItemAdded()
        {
            var expectedItem = new ItemModel { Id = Guid.Parse("a09c0705-b162-4443-b497-9812e6b5c5aa"), Text = "Another coffee" };

            var actualStatusCode = await _controller.ResolveResult(controller => controller.PostAsync(expectedItem));
            
            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
        
        [Test]
        public async Task PutItem_ExistingItem_ItemUpdated()
        {
            var updateItem = new ItemModel { Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d2"), Text = "Add some coffee" };

            var (actualStatusCode, actualItem) =
                await _controller.ResolveResult<ItemListController, ItemModel>(controller => controller.PutAsync(updateItem.Id, updateItem));

            Assert.That(actualItem, Is.EqualTo(updateItem).UsingItemModelComparer());
            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task PutItem_UnexistingItem_ItemAdded()
        {
            var updateItem = new ItemModel { Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d2"), Text = "Add some coffee" };

            var (actualStatusCode, actualItem) =
                await _controller.ResolveResult<ItemListController, ItemModel>(controller => controller.PutAsync(updateItem.Id, updateItem));

            Assert.That(actualItem, Is.EqualTo(updateItem).UsingItemModelComparer());
            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task DeleteItem_ItemDeleted()
        {
            var guid = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3");

            var actualStatusCode = await _controller.ResolveResult(controller => controller.DeleteAsync(guid));
            
            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

    }
}
