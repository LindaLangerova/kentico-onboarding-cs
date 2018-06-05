﻿using System;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Contract.Models;
using TodoApp.Contract.Services;
using TodoApp.Contract.Tests.Utilities;
using TodoApp.Services.Creators;

namespace TodoApp.Services.Test.ItemServices
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
            _itemCreator = new ItemCreator(idGenerator);
        }

        [Test]
        public void SetItem_ValidItem_ReturnTrueAndCorrectItem()
        {
            var validItem = new Item {Text = "Make a coffee"};
            var result = _itemCreator.SetItem(validItem);

            Assert.That(result.Id, Is.EqualTo(FakeItem.Id));
        }
    }
}