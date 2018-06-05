using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Api.Controllers;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services;
using TodoApp.Contract.Tests.Utilities;
using TodoApp.Contract.Tests.Utilities.ActionsResolution;
using TodoApp.Contract.Tests.Utilities.Comparers;

namespace TodoApp.Api.Tests.Controllers
{
    public class ItemsControllerTests : TestBase
    {
        private ItemsController _controller;
        private IItemRepository _repository;
        private IItemCreator _itemCreator;
        private IItemCacher _itemCacher;
        private IRouteNameProvider _routeNameProvider;

        private static readonly Item FakeItem =
            new Item {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a coffee"};

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IItemRepository>();

            var urlGenerator = Substitute.For<IUrlGenerator>();
            _routeNameProvider = Substitute.For<IRouteNameProvider>();

            urlGenerator.GetItemUrl(FakeItem.Id).Returns($"api/v1/itemlist/{FakeItem.Id}");

            _itemCreator = Substitute.For<IItemCreator>();
            _itemCacher = Substitute.For<IItemCacher>();

            _controller = new ItemsController(_repository, urlGenerator, _itemCreator, _itemCacher)
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
            _itemCacher.ItemExists(FakeItem.Id).Returns(true);
            _itemCacher.GetItem(FakeItem.Id).Returns(FakeItem);

            _itemCacher.ItemExists(Guid.Empty).Returns(false);

            var response = await _controller.ResolveAction(controller => controller.GetAsync(FakeItem.Id)).BeItReducedResponse<Item>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK))
                  .AndThat(response.Content, Is.EqualTo(FakeItem).UsingItemModelComparer());
        }

        [Test]
        public async Task GetItem_NotExistingId_NotFoundReturned()
        {
            _itemCacher.ItemExists(Guid.Empty).Returns(false);

            var response = await _controller.ResolveAction(controller => controller.GetAsync(Guid.Empty)).BeItReducedResponse();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task PostNewItem_ValidItem_ItemAdded()
        {
            var expectedRoute = new Uri($"api/v1/itemlist/{FakeItem.Id}", UriKind.Relative);
            var reffedItem = new Item {Text = FakeItem.Text};
            _itemCreator.SetItem(reffedItem).Returns(FakeItem);

            var response = await _controller.ResolveAction(controller => controller.PostAsync(reffedItem)).BeItReducedResponse<Item>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created))
                  .AndThat(response.Location, Is.EqualTo(expectedRoute))
                  .AndThat(response.Content, Is.EqualTo(FakeItem));
        }

        [Test]
        public async Task PostNewItem_InvalidItem_BadRequestReturned()
        {
            _itemCreator.SetItem(FakeItem).Returns(FakeItem);
            _repository.AddAsync(FakeItem).Returns(Task.FromResult(FakeItem));

            var response = await _controller.ResolveAction(controller => controller.PostAsync(FakeItem)).BeItReducedResponse<Item>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task PutItem_ExistingItem_ItemUpdated()
        {
            _repository.UpdateAsync(FakeItem.Id, FakeItem).Returns(FakeItem);

            var response = await _controller.ResolveAction(controller => controller.PutAsync(FakeItem.Id, FakeItem))
                                            .BeItReducedResponse<Item>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK))
                  .AndThat(response.Content, Is.EqualTo(FakeItem).UsingItemModelComparer());
        }

        [Test]
        public async Task DeleteItem_ItemDeleted()
        {
            _repository.DeleteAsync(FakeItem.Id).Returns(Task.FromResult(HttpStatusCode.NoContent));
            var response = await _controller.ResolveAction(controller => controller.DeleteAsync(FakeItem.Id)).BeItReducedResponse();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }
    }
}
