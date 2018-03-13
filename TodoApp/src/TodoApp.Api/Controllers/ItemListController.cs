using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Web.Http;
using TodoApp.Contract;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;

namespace TodoApp.Api.Controllers
{
    [ApiVersion("1.0")]
    public class ItemListController : ApiController
    {
        private readonly IItemRepository _repository;
        private readonly IItemUrlObtainer _urlObtainer;

        public ItemListController(IItemRepository repository, IItemUrlObtainer urlObtainer)
        {
            _repository = repository;
            _urlObtainer = urlObtainer;
        }

        public async Task<IHttpActionResult> GetAllAsync() => await Task.FromResult(Ok(_repository.GetAll()));

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            var item = _repository.Get(id);
            return await Task.FromResult(Ok(item));
        }

        public async Task<IHttpActionResult> PostAsync(Item item)
        {
            var newItem = _repository.Add(item);
            var location = _urlObtainer.GetItemUrl(item.Id);
            return await Task.FromResult(Created(location, newItem));
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, Item item)
        {
            var updatedItem = _repository.Update(id, item);
            return await Task.FromResult(Ok(updatedItem));
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            _repository.Delete(id);
            return await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
        }
    }
}