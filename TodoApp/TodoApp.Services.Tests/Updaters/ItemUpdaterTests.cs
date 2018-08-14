using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Driver;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using TodoApp.Api.Controllers;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services.Creators;
using TodoApp.Contract.Services.Generators;
using TodoApp.Contract.Services.Providers;
using TodoApp.Contract.Services.Updaters;
using TodoApp.Contract.Tests.Utilities;
using TodoApp.Services.Generators;
using TodoApp.Services.Updaters;

namespace TodoApp.Services.Tests.Updaters
{
    class ItemUpdaterTests : TestBase
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
        private static readonly DateTime TimeNow = new DateTime(2,2,2);


        [SetUp]
        public void SetUp()
        {
            var dateTimeGenerator = Substitute.For<IDateTimeGenerator>();
            dateTimeGenerator.GetActualDateTime().Returns(TimeNow);
            _itemUpdater = new ItemUpdater(dateTimeGenerator);
        }

        [Test]
        public void UpdateItem_ItemWithTheUpdatedTimeOfLastChangeReturned()
        {
            var mongoCollection = Substitute.For<IMongoCollection<Item>>();
            
            var receivedItem = _itemUpdater.UpdateItemInCollection(mongoCollection, DefaultId, DefaultItem).Result;

            Assert.That(receivedItem.LastChange, Is.EqualTo(TimeNow));
        }
    }
}
