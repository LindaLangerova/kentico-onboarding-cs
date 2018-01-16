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
    [ApiVersion("1")]
    [RoutePrefix("api/v{version:apiVersion}/itemlist")]
    public class ItemListControllerAsync : ApiController
    {
        public static ItemModel[] ItemList =
        {
            new ItemModel {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a cofee"},
            new ItemModel {Id = Guid.Parse("55b0d56d-48d7-4f93-bd73-e4b801e26faa"), Text = "Make second coffee"},
            new ItemModel {Id = Guid.Parse("886c2ea5-a639-4334-8c51-d3ee4e49acb9"), Text = "Make third cofffee"},
            new ItemModel {Id = Guid.Parse("250be0cc-438e-46cc-a0fe-549f4d3409e2"), Text = "Coffee is awesome as well as Kentico is"}
        };

        
        public async Task<IHttpActionResult> GetAll()
            => await Task.FromResult(Ok(ItemList));
        
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(Guid id) 
            => await Task.FromResult(Ok(ItemList[0]));

        [Route("")]
        public async Task<IHttpActionResult> Post(ItemModel item) 
            => await Task.FromResult(Created($"api/itemlist/{ItemList[0].Id}", ItemList[0]));

        [Route("{id}")]
        public async Task<IHttpActionResult> Put(Guid id, ItemModel item) 
            => await Task.FromResult(Ok(ItemList[2]));

        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(Guid id) 
            => await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
    }
}