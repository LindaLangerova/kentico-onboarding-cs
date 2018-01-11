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
        private static ItemModel[] ItemList =
        {
            new ItemModel {Id = Guid.Parse("00000000000000000000000000000000"), Text = "Make a cofee"},
            new ItemModel {Id = Guid.Parse("00000000000000000000000000000001"), Text = "Make second coffee"},
            new ItemModel {Id = Guid.Parse("00000000000000000000000000000002"), Text = "Make third cofffee"},
            new ItemModel {Id = Guid.Parse("00000000000000000000000000000003"), Text = "Coffee is awesome as well as Kentico is"}
        };

        public async Task<IHttpActionResult> GetAllItems()
            => await Task.FromResult(Ok(ItemList));
        
        public async Task<IHttpActionResult> GetItem(Guid id)
        {
            var item = ItemList.FirstOrDefault(t => t.Id == id);
            return await Task.FromResult(Ok(item));
        }

        public async Task<IHttpActionResult> PostNewItem(ItemModel item)
        {
            if(ItemList.FirstOrDefault(t => t.Id == item.Id) != null)
            {
                return await Task.FromResult(Conflict());
            };
            return await Task.FromResult(Created("api/itemlist/5", item));
        } 

        public async Task<IHttpActionResult> PutItem(Guid id, ItemModel item)
        {
            var selectedItem = ItemList.FirstOrDefault(t => t.Id == id);
            if (selectedItem != null) selectedItem.Text = item.Text;
            else
            {
                //ItemList.Add(item);
            }
            return await Task.FromResult(Ok(item));
        }
        
        public async Task<IHttpActionResult> DeleteItem(Guid id) => await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
    }
}