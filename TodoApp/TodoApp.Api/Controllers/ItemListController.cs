using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Web.Http;
using TodoApp.Api.Models;

namespace TodoApp.Api.Controllers
{
    [ApiVersion("1.0")]
    public class ItemListController : ApiController
    {
         private static readonly ItemModel[] DefaultItems =
        {
            new ItemModel {Id = "0", Text = "Make a cofee"},
            new ItemModel {Id = "1", Text = "Make second coffee"},
            new ItemModel {Id = "2", Text = "Make third cofffee"},
            new ItemModel {Id = "3", Text = "Coffee is awesome as well as Kentico is"}
        };
        public static readonly List<ItemModel> ItemList = new List<ItemModel>(DefaultItems);

        [HttpGet]
        public IEnumerable<ItemModel> GetAllItems()
        {
            return ItemList;
        }

        [HttpGet]
        public ItemModel GetItem(string id)
        {
            var item = ItemList.FirstOrDefault(t => t.Id == id);

            return item;
        }

        [HttpPost]
        public IHttpActionResult AddNewItem(ItemModel item)
        {
            ItemList.Add(item);

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateItem(string id, ItemModel item)
        {
            var selectedItem = ItemList.FirstOrDefault(t => t.Id == id);
            if (selectedItem != null) selectedItem.Text = item.Text;

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteItem(string id)
        {
            var selectedItem = ItemList.FirstOrDefault(t => t.Id == id);
            ItemList.Remove(selectedItem);

            return Ok();
        }
    }
}