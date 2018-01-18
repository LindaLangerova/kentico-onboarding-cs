using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TodoApp.Api.Controllers;
using TodoApp.Api.Models;
using TodoApp.Api.Tests.Utilities;
using TodoApp.Api.Tests.Utilities.ActionsResolution;
using TodoApp.Api.Tests.Utilities.Comparers;

namespace TodoApp.Api.Tests.Controllers
{
    public class ItemListControllerTest : TestBase
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
                new ItemModel {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a coffee"},
                new ItemModel {Id = Guid.Parse("55b0d56d-48d7-4f93-bd73-e4b801e26faa"), Text = "Make second coffee"},
                new ItemModel {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d2"), Text = "Add some coffee"},
                new ItemModel {Id = Guid.Parse("250be0cc-438e-46cc-a0fe-549f4d3409e2"), Text = "Coffee overflow"}
            };

            var response = await _controller
                .ResolveAction(controller => controller.GetAllAsync())
                .BeItReducedResponse<ItemModel[]>();

            Assert
                .That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK))
                .AndThat(response.Content, Is.EqualTo(expectedItems).UsingItemModelComparer());
        }

        [Test]
        public async Task GetItem_ExistingId_ItemReturned()
        {
            var guid = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3");
            var expectedItem = new ItemModel { Id = guid, Text = "Make a coffee" };

            var response = await _controller
                .ResolveAction(controller => controller.GetAsync(guid))
                .BeItReducedResponse<ItemModel>();

            Assert
                .That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK))
                .AndThat(response.Content, Is.EqualTo(expectedItem).UsingItemModelComparer());
        }

        [Test]
        public async Task PostNewItem_UniqueItem_ItemAdded()
        {
            var expectedItem = new ItemModel { Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a coffee" };
            var expectedRoute = new Uri("api/itemlist/c5cc89a0-ab8d-4328-9000-3da679ec02d3", UriKind.Relative);

            var response = await _controller
                .ResolveAction(controller => controller.PostAsync(expectedItem))
                .BeItReducedResponse<ItemModel>();

            Assert
                .That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created))
                .AndThat(response.Location, Is.EqualTo(expectedRoute))
                .AndThat(response.Content, Is.EqualTo(expectedItem).UsingItemModelComparer());
        }

        [Test]
        public async Task PutItem_ExistingItem_ItemUpdated()
        {
            var updateItem = new ItemModel { Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d2"), Text = "Add some coffee" };

            var response = await _controller
                .ResolveAction(controller => controller.PutAsync(updateItem.Id, updateItem))
                .BeItReducedResponse<ItemModel>();

            Assert
                .That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK))
                .AndThat(response.Content, Is.EqualTo(updateItem).UsingItemModelComparer());
        }

        [Test]
        public async Task DeleteItem_ItemDeleted()
        {
            var guid = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3");

            var response = await _controller
                .ResolveAction(controller => controller.DeleteAsync(guid))
                .BeItReducedResponse();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

    }
}
