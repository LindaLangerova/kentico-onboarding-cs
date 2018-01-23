using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Web.Http;
using TodoApp.Contract.Models;
using TodoApp.Data.Repositories;

namespace TodoApp.Api.Controllers
{
    [ApiVersion("1")]
    [RoutePrefix("api/v{version:apiVersion}/itemlist")]
    [Route("")]
    public class ItemListController : ApiController
    {
        private const string Id = "{id}";

        private readonly IItemRepository _repository;

        public ItemListController(IItemRepository repository)
        {
            _repository = repository;
        }


        public async Task<IHttpActionResult> GetAllAsync()
            => await Task.FromResult(Ok(_repository.GetAll()));
        /**
        [Route(Id)]
        public async Task<IHttpActionResult> GetAsync(Guid id) 
            => await Task.FromResult(Ok(ItemList[0]));
        
        public async Task<IHttpActionResult> PostAsync(ItemModel item) 
            => await Task.FromResult(Created($"api/itemlist/{ItemList[0].Id}", ItemList[0]));

        [Route(Id)]
        public async Task<IHttpActionResult> PutAsync(Guid id, ItemModel item) 
            => await Task.FromResult(Ok(ItemList[2]));

        [Route(Id)]
        public async Task<IHttpActionResult> DeleteAsync(Guid id) 
            => await Task.FromResult(StatusCode(HttpStatusCode.NoContent));**/
    }
}