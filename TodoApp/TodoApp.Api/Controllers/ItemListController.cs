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
        
        [Route(Id)]
        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            var item = _repository.Get(id);
            if (item == null)
                return NotFound();

            return await Task.FromResult(Ok(item));
        }

        public async Task<IHttpActionResult> PostAsync(ItemModel item)
        {
            if (item?.Text == null)
                return BadRequest();

            var newItem = _repository.Add(item);

            return await Task.FromResult(Created($"api/itemlist/{newItem.Id}", newItem));
        }
        
        [Route(Id)]
        public async Task<IHttpActionResult> PutAsync(Guid id, ItemModel item)
        {
            if (item?.Text == null || item.Id != id)
                return BadRequest();
            var updatedItem = _repository.Update(id, item);
            return await Task.FromResult(Ok(updatedItem));
        }

        [Route(Id)]
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            _repository.Delete(id);
            return await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
        }
    }
}