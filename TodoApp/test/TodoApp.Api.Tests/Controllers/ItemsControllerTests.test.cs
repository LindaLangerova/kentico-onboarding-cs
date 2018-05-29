using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NSubstitute;
using NUnit.Framework;
using Todo.App.Services.ItemServices;
using Todo.App.Services.UrlServices;
using TodoApp.Api.Controllers;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Tests.Utilities;
using TodoApp.Contract.Tests.Utilities.ActionsResolution;
using TodoApp.Contract.Tests.Utilities.Comparers;

namespace TodoApp.Api.Tests.Controllers
{
    public class ItemsControllerTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IItemRepository>();

            var urlGenerator = Substitute.For<IUrlGenerator>();

            urlGenerator.GetItemUrl(Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), RouteConfig.DefaultApi)
                        .Returns("api/v1/itemlist/c5cc89a0-ab8d-4328-9000-3da679ec02d3");

            _itemCreator = Substitute.For<IItemCreator>();

            _controller = new ItemsController(_repository, urlGenerator, _itemCreator)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        private ItemsController _controller;
        private IItemRepository _repository;
        private IItemCreator _itemCreator;

        [Test]
        public async Task DeleteItem_ItemDeleted()
        {
            var id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3");
            _repository.DeleteAsync(id).Returns(Task.FromResult(HttpStatusCode.NoContent));
            var response = await _controller.ResolveAction(controller => controller.DeleteAsync(id)).BeItReducedResponse();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task GetAllItems_AllItemsReturned()
        {
            var expectedItems = new[]
            {
                new Item {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a coffee"},
                new Item {Id = Guid.Parse("55b0d56d-48d7-4f93-bd73-e4b801e26faa"), Text = "Make second coffee"},
                new Item {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d2"), Text = "Add some coffee"},
                new Item {Id = Guid.Parse("250be0cc-438e-46cc-a0fe-549f4d3409e2"), Text = "Coffee overflow"}
            };

            _repository.GetAllAsync().Returns(expectedItems.ToList());

            var response = await _controller.ResolveAction(controller => controller.GetAllAsync()).BeItReducedResponse<List<Item>>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK))
                  .AndThat(response.Content, Is.EqualTo(expectedItems.ToList()).UsingItemModelComparer());
        }

        [Test]
        public async Task GetItem_ExistingId_ItemReturned()
        {
            var id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3");
            var expectedItem = new Item {Id = id, Text = "Make a coffee"};
            _repository.GetAsync(id).Returns(Task.FromResult(expectedItem));

            var response = await _controller.ResolveAction(controller => controller.GetAsync(id)).BeItReducedResponse<Item>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK))
                  .AndThat(response.Content, Is.EqualTo(expectedItem).UsingItemModelComparer());
        }

        [Test]
        public async Task PostNewItem_UniqueItem_ItemAdded()
        {
            var id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3");
            var expectedItem = new Item {Id = id, Text = "Make a coffee"};
            var expectedRoute = new Uri($"api/v1/itemlist/{id}", UriKind.Relative);
            
            _itemCreator.SetItem(Arg.Is<Item>(item => item.IsValidForCreating()))
                        .Returns(expectedItem);
            _repository.AddAsync(expectedItem).Returns(expectedItem);

            var response = await _controller.ResolveAction(controller => controller.PostAsync(new Item{Text = expectedItem .Text}))
                                            .BeItReducedResponse<Item>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created))
                  .AndThat(response.Location, Is.EqualTo(expectedRoute))
                  .AndThat(response.Content, Is.EqualTo(expectedItem));
        }

        [Test]
        public async Task PutItem_ExistingItem_ItemUpdated()
        {
            var id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3");
            var updateItem = new Item {Id = id, Text = "Make a coffee"};
            _repository.UpdateAsync(id, updateItem).Returns(updateItem);

            var response = await _controller.ResolveAction(controller => controller.PutAsync(updateItem.Id, updateItem))
                                            .BeItReducedResponse<Item>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK))
                  .AndThat(response.Content, Is.EqualTo(updateItem).UsingItemModelComparer());
        }
    }
}
