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
            => await Task.FromResult(Ok(_repository.Get(id)));
        
        public async Task<IHttpActionResult> PostAsync(ItemModel item)
        {
            var newItem = _repository.Add(item);
            return await Task.FromResult(Created($"api/itemlist/{newItem.Id}", newItem));
        }
        
        [Route(Id)]
        public async Task<IHttpActionResult> PutAsync(Guid id, ItemModel item) 
            => await Task.FromResult(Ok(_repository.Update(id,item)));

        [Route(Id)]
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            _repository.Delete(id);
            return await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
        }
    }
}