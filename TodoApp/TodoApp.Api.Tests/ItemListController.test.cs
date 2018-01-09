using System.Web.Http.Results;
using NUnit.Framework;
using TodoApp.Api.Controllers;
using TodoApp.Api.Models;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TodoApp.Api.Tests
{
    [TestFixture]
    public class ItemListControllerTest
    {
        private ItemListController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new ItemListController();
        }

        [Test]
        public void GetAllItemsTest()
        {
            var allItems = _controller.GetAllItems();

            Assert.AreEqual(allItems, ItemListController.ItemList);
        }

        [Test]
        public void GetItemTest()
        {
            var item = _controller.GetItem("0");

            Assert.AreEqual("Make a cofee", item.Text);
        }

        [Test]
        public void AddNewItemTest()
        {
            ItemModel item = new ItemModel {Id = "4", Text = "New data"};
            var result = _controller.AddNewItem(item);

            Assert.AreSame(result.GetType(), typeof(OkResult));
        }

        [Test]
        public void UpdateItemTest()
        {
            var result = _controller.UpdateItem("0", new ItemModel {Id = "0", Text ="New data"});

            Assert.AreSame(result.GetType(), typeof(OkResult));
        }

        [Test]
        public void DeleteItemTest()
        {
            var result = _controller.DeleteItem("2");

            Assert.AreSame(result.GetType(), typeof(OkResult));
        }

    }
}
