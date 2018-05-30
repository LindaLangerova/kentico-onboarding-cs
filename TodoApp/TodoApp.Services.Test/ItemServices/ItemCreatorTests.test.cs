using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Todo.App.Services.IdServices;
using Todo.App.Services.ItemServices;
using TodoApp.Contract.Models;
using TodoApp.Contract.Tests.Utilities;

namespace TodoApp.Services.Test.ItemServices
{
    class ItemCreatorTests: TestBase
    {
        private IItemCreator _itemCreator;
        private static readonly Item FakeItem =
            new Item { Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a coffee" };


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
            var validItem = new Item{Text = "Make a coffee" };
            var result = _itemCreator.SetItem(ref validItem);

            Assert.That(result, Is.EqualTo(true)).AndThat(validItem.Id, Is.EqualTo(FakeItem.Id));
        }


    }
}
