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
    public class ItemListController : ApiController
    {
        private readonly IItemRepository _repository;
        private readonly IUrlGenerator _urlGenerator;

        public ItemListController(IItemRepository repository, IUrlGenerator urlGenerator)
        {
            _repository = repository;
            _urlGenerator = urlGenerator;
        }

        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await Task.FromResult(_repository.GetAll());
            if (_repository == null) return NotFound();
            return Ok(result);
        }

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            var item = await Task.FromResult(_repository.Get(id));
            if (item == null) return NotFound();
            return Ok(item);
        }

        public async Task<IHttpActionResult> PostAsync(Item item)
        {
            if (item.Text == null) return BadRequest();
            var newItemText = _repository.Add(item);
            var location = _urlGenerator.GetItemUrl(item.Id);
            return await Task.FromResult(Created(location, newItemText));
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
