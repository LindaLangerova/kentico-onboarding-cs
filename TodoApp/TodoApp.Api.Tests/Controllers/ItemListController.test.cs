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
                new ItemModel {Id = Guid.Parse("00000000000000000000000000000000"), Text = "Make a cofee"},
                new ItemModel {Id = Guid.Parse("00000000000000000000000000000001"), Text = "Make second coffee"},
                new ItemModel {Id = Guid.Parse("00000000000000000000000000000002"), Text = "Make third cofffee"},
                new ItemModel {Id = Guid.Parse("00000000000000000000000000000003"), Text = "Coffee is awesome as well as Kentico is"}
            };

            var result = await _controller.GetAllItems();
            var message = await result.ExecuteAsync(CancellationToken.None);
            var actualStatusCode = message.StatusCode;
            message.TryGetContentValue(out ItemModel[] actualItems);

            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualItems, Is.EqualTo(expectedItems).UsingItemModelComparer());
        }

        [Test]
        public async Task GetItem_ExistingId_ItemReturned()
        {
            var expectedItem = new ItemModel {Id = Guid.Parse("00000000000000000000000000000000"), Text = "Make a cofee"};

            var result = await _controller.GetItem(Guid.Parse("00000000000000000000000000000000"));
            var message = await result.ExecuteAsync(CancellationToken.None);
            var actualStatusCode = message.StatusCode;
            message.TryGetContentValue(out ItemModel actualItem);

            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualItem, Is.EqualTo(expectedItem).UsingItemModelComparer());
        }

        [Test]
        public async Task PostNewItem_UniqueItem_ItemAdded()
        {
            var expectedItem = new ItemModel { Id = Guid.Parse("00000000000000000000000000000004"), Text = "Another coffee" };

            var result = await _controller.PostNewItem(expectedItem);
            var message = await result.ExecuteAsync(CancellationToken.None);
            var actualStatusCode = message.StatusCode;
            message.TryGetContentValue(out ItemModel actualItem);

            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(actualItem, Is.EqualTo(expectedItem).UsingItemModelComparer());
        }
        
        [Test]
        public async Task PutItem_ExistingItem_ItemUpdated()
        {
            var updateItem = new ItemModel { Id = Guid.Parse("00000000000000000000000000000000"), Text = "Add some milk" };

            var result = await _controller.PutItem(updateItem.Id, updateItem);
            var message = await result.ExecuteAsync(CancellationToken.None);
            var actualStatusCode = message.StatusCode;
            message.TryGetContentValue(out ItemModel actualItem);
            
            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualItem, Is.EqualTo(updateItem).UsingItemModelComparer());
        }

        public async Task PutItem_UnexistingItem_ItemAdded()
        {
            var updateItem = new ItemModel { Id = Guid.Parse("00000000000000000000000000000000"), Text = "Coffee overflow" };

            var result = await _controller.PutItem(updateItem.Id, updateItem);
            var message = await result.ExecuteAsync(CancellationToken.None);
            var actualStatusCode = message.StatusCode;
            message.TryGetContentValue(out ItemModel actualItem);

            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualItem, Is.EqualTo(updateItem).UsingItemModelComparer());
        }

        [Test]
        public async Task DeleteItem_ItemDeleted()
        {
            var result = await _controller.DeleteItem(Guid.Parse("00000000000000000000000000000000"));
            var message = await result.ExecuteAsync(CancellationToken.None);
            var actualStatusCode = message.StatusCode;
            
            Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

    }
}
