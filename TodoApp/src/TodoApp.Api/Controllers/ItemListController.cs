using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Web.Http;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services;

namespace TodoApp.Api.Controllers
{
    [EnableCors("*", "*", "*")]
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
            var result = await _repository.GetAll();
            if (result == null) return NotFound();
            return await Task.FromResult(Ok(result));
        }

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            var item = await _repository.Get(id);

            if (item == null)
                return NotFound();

            return await Task.FromResult(Ok(item));
        }

        public async Task<IHttpActionResult> PostAsync(Item item)
        {
            if (item?.Text == null)
                return BadRequest();

            var newItemText = await _repository.Add(item);
            var location = _urlGenerator.GetItemUrl(item.Id);
            return await Task.FromResult(Created(location, newItemText));
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, Item item)
        {
            var updatedItem = await _repository.Update(id, item);
            return await Task.FromResult(Ok(updatedItem));
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await _repository.Delete(id);
            return await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
        }   
    }
}
