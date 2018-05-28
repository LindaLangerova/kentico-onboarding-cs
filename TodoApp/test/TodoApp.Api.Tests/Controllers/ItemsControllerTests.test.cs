using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Api.Controllers;
using TodoApp.Api.Tests.Utilities;
using TodoApp.Api.Tests.Utilities.ActionsResolution;
using TodoApp.Api.Tests.Utilities.Comparers;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services;

namespace TodoApp.Api.Tests.Controllers
{
    public class ItemsControllerTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            var urlGenerator = Substitute.For<IUrlGenerator>();

            urlGenerator.GetItemUrl(Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d8"))
                        .Returns("api/v1.1/itemlist/c5cc89a0-ab8d-4328-9000-3da679ec02d3");

            _repository = Substitute.For<IItemRepository>();

            _controller = new ItemsController(_repository, urlGenerator)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        private static readonly Item[] ReferencedItems =
        {
            new Item {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a coffee"},
            new Item {Id = Guid.Parse("55b0d56d-48d7-4f93-bd73-e4b801e26faa"), Text = "Make second coffee"},
            new Item {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d2"), Text = "Add some coffee"},
            new Item {Id = Guid.Parse("250be0cc-438e-46cc-a0fe-549f4d3409e2"), Text = "Coffee overflow"}
        };

        private ItemsController _controller;
        private IItemRepository _repository;

        [Test]
        public async Task DeleteItem_ItemDeleted()
        {
            var id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3");

            var response = await _controller.ResolveAction(controller => controller.DeleteAsync(id)).BeItReducedResponse();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task GetAllItems_AllItemsReturned()
        {
            _repository.GetAllAsync().Returns(Task.FromResult(ReferencedItems));

            var expectedItems = ReferencedItems;

            var response = await _controller.ResolveAction(controller => controller.GetAllAsync()).BeItReducedResponse<Item[]>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK))
                  .AndThat(response.Content, Is.EqualTo(expectedItems).UsingItemModelComparer());
        }

        [Test]
        public async Task GetItem_ExistingId_ItemReturned()
        {
            var fakeId = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3");
            var fakeItem = new Item {Id = fakeId, Text = "Make a coffee"};

            _repository.GetAsync(fakeId).Returns(Task.FromResult(fakeItem));

            var expectedItem = await Task.FromResult(fakeItem);

            var response = await _controller.ResolveAction(controller => controller.GetAsync(fakeId)).BeItReducedResponse<Item>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK))
                  .AndThat(response.Content, Is.EqualTo(expectedItem).UsingItemModelComparer());
        }

        [Test]
        public async Task PostNewItem_UniqueItem_ItemAdded()
        {
            var id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3");
            var expectedItem = new Item {Id = id, Text = "Make a coffee"};

            var fakeId = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d8");
            var fakeItem = new Item {Id = fakeId, Text = "Make a coffee 2"};

            _repository.AddAsync(Arg.Is<Item>(item => item.Id.ToString() == "c5cc89a0-ab8d-4328-9000-3da679ec02d8"
                                                 && item.Text == "Make a coffee 2"))
                       .Returns(Task.FromResult(expectedItem));

            var expectedRoute = new Uri($"api/v1.1/itemlist/{id}", UriKind.Relative);

            var response = await _controller.ResolveAction(controller => controller.PostAsync(fakeItem)).BeItReducedResponse<Item>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created))
                  .AndThat(response.Location, Is.EqualTo(expectedRoute))
                  .AndThat(response.Content, Is.EqualTo(expectedItem).UsingItemModelComparer());
        }

        [Test]
        public async Task PutItem_ExistingItem_ItemUpdated()
        {
            var id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3");
            var updateItem = new Item {Id = id, Text = "Make a coffee"};

            _repository.UpdateAsync(id,
                               Arg.Is<Item>(item => item.Equals(updateItem) ))
                       .Returns(Task.FromResult(updateItem));

            var response = await _controller.ResolveAction(controller => controller.PutAsync(updateItem.Id, updateItem))
                                            .BeItReducedResponse<Item>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK))
                  .AndThat(response.Content, Is.EqualTo(updateItem).UsingItemModelComparer());
        }
    }
}
