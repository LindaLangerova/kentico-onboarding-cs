using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Web.Http;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services;

namespace TodoApp.Api.Controllers
{
    [ApiVersion("1.0")]
    public class ItemsController : ApiController
    {
        private readonly IItemRepository _repository;
        private readonly IUrlGenerator _urlGenerator;

        public ItemsController(IItemRepository repository, IUrlGenerator urlGenerator)
        {
            _repository = repository;
            _urlGenerator = urlGenerator;
        }

        public async Task<IHttpActionResult> GetAllAsync()
            => Ok(await _repository.GetAllAsync());

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            var item = await _repository.GetAsync(id);

            return Ok(item);
        }

        public async Task<IHttpActionResult> PostAsync(Item item)
        {
            var newItem = await _repository.AddAsync(item);
            var location = _urlGenerator.GetItemUrl(item.Id);

            return Created(location, newItem);
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, Item item)
        {
            var updatedItem = await _repository.UpdateAsync(id, item);

            return Ok(updatedItem);
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            _repository.Delete(id);

            return await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
        }
    }
}
