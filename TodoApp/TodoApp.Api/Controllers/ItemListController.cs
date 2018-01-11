using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.Web.Http;
using TodoApp.Api.Models;

namespace TodoApp.Api.Controllers
{
    [ApiVersion("1.0")]
    public class ItemListController : ApiController
    {
        private ItemModel[] ItemList =
        {
            new ItemModel {Id = "0", Text = "Make a cofee"},
            new ItemModel {Id = "1", Text = "Make second coffee"},
            new ItemModel {Id = "2", Text = "Make third cofffee"},
            new ItemModel {Id = "3", Text = "Coffee is awesome as well as Kentico is"}
        };

        public async Task<IHttpActionResult> GetAllItems()
            => await Task.FromResult(Ok(ItemList));
        
        public async Task<IHttpActionResult> GetItem(string id)
        {
            var item = ItemList.FirstOrDefault(t => t.Id == id);
            return await Task.FromResult(Ok(item));
        }

        public async Task<IHttpActionResult> PostNewItem(ItemModel item) => await Task.FromResult(Created("api/itemlist/5", item));

        public async Task<IHttpActionResult> PutItem(string id, ItemModel item)
        {
            var selectedItem = ItemList.FirstOrDefault(t => t.Id == id);
            if (selectedItem != null) selectedItem.Text = item.Text;
            else
            {
                //addItem
            }
            return await Task.FromResult(Ok(item));
        }
        
        public async Task<IHttpActionResult> DeleteItem(string id) => await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
    }
}