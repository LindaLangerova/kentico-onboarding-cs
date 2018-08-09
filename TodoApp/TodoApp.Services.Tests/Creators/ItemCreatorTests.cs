using System;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Contract.Models;
using TodoApp.Contract.Services.Creators;
using TodoApp.Contract.Services.Generators;
using TodoApp.Contract.Tests.Utilities;
using TodoApp.Contract.Tests.Utilities.Comparers;
using TodoApp.Services.Creators;

namespace TodoApp.Services.Tests.Creators
{
    internal class ItemCreatorTests : TestBase
    {
        private IItemCreator _itemCreator;

        private static readonly Item FakeItem =
            new Item {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a coffee"};

        [SetUp]
        public void SetUp()
        {
            var idGenerator = Substitute.For<IIdGenerator>();
            idGenerator.GenerateId().Returns(FakeItem.Id);
            var dateTimeGenerator = Substitute.For<IDateTimeGenerator>();
            _itemCreator = new ItemCreator(idGenerator, dateTimeGenerator);
        }

        [Test]
        public void SetItem_ValidItem_CorrectItem()
        {
            var validItem = new Item {Text = "Make a coffee", CreatedAt = DateTime.MinValue, LastChange = DateTime.MinValue};
            var result = _itemCreator.SetItem(validItem);

            Assert.That(result, Is.EqualTo(FakeItem).UsingItemModelComparer());
        }
    }
}
