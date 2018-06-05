using System;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services;
using TodoApp.Contract.Tests.Utilities;
using TodoApp.Services.Providers;

namespace TodoApp.Services.Test.ItemServices
{
    internal class ItemCacherTests : TestBase
    {
        private IItemCacher _itemCacher;
        private IItemRepository _repository;
        

        private static readonly Item FakeItem =
            new Item {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a coffee"};

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IItemRepository>();
            _repository.GetAsync(FakeItem.Id).Returns(FakeItem);

            _itemCacher = new ItemCacher(_repository);
        }

        [Test]
        public async Task GetItem_ExistingItemId_ItemReturned()
        {
            var receivedItem = await _itemCacher.GetItem(FakeItem.Id);

            Assert.That(receivedItem, Is.EqualTo(FakeItem));
        }

        [Test]
        public async Task GetItem_NotExistingItemId_NullReturned()
        {
            var receivedItem = await _itemCacher.GetItem(Guid.Empty);

            Assert.That(receivedItem, Is.EqualTo(null));
        }

        [Test]
        public async Task ItemExists_ExistingItemId_TrueReturned()
        {
            var result = await _itemCacher.ItemExists(FakeItem.Id);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public async Task ItemExists_NotExistingItemId_FalseReturned()
        {
            var result = await _itemCacher.ItemExists(Guid.Empty);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public async Task ItemIsCached_ExistingItem_CalledRepositoryOnlyOnce()
        {
            await _itemCacher.ItemExists(FakeItem.Id);
            await _itemCacher.ItemExists(FakeItem.Id);
            
            Assert.That(_repository.ReceivedCalls().Count(), Is.EqualTo(1));
        }
    }
}
