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
            => await Task.FromResult(Ok(_repository.GetAll()));

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            var item = _repository.Get(id);
            return await Task.FromResult(Ok(item));
        }

        public async Task<IHttpActionResult> PostAsync(Item item)
        {
            var newItem = _repository.Add(item);
            var location = _urlGenerator.GetItemUrl(item.Id);
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
