using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services.Generators;
using TodoApp.Contract.Services.Updaters;
using TodoApp.Contract.Tests.Utilities;
using TodoApp.Services.Updaters;

namespace TodoApp.Services.Tests.Updaters
{
    internal class ItemUpdaterTests : TestBase
    {
        private IItemUpdater _itemUpdater;
        private static readonly Guid DefaultId = new Guid("60407204-e3f2-46c1-bfc4-cc51f35f6e3c");

        private static readonly Item DefaultItem = new Item
        {
            Id = DefaultId,
            Text = "Item",
            CreatedAt = DateTime.MinValue,
            LastChange = DateTime.MinValue
        };

        private static readonly DateTime TimeNow = new DateTime(2, 2, 2);

        [SetUp]
        public void SetUp()
        {
            var dateTimeGenerator = Substitute.For<IDateTimeGenerator>();
            dateTimeGenerator.GetActualDateTime().Returns(TimeNow);
            var itemRepository = Substitute.For<IItemRepository>();

            _itemUpdater = new ItemUpdater(itemRepository, dateTimeGenerator);
        }

        [Test]
        public void UpdateItem_ItemWithTheUpdatedTimeOfLastChangeReturned()
        {
            var receivedItem = _itemUpdater.UpdateItem(DefaultId, DefaultItem).Result;

            Assert.That(receivedItem.LastChange, Is.EqualTo(TimeNow));
        }
    }
}
