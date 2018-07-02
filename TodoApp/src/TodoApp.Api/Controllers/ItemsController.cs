using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Web.Http;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services.Creators;
using TodoApp.Contract.Services.Generators;
using TodoApp.Contract.Services.Providers;
using TodoApp.Services.Validators;

namespace TodoApp.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [ApiVersion("1.0")]
    public class ItemsController : ApiController
    {
        private readonly IItemCacher _itemCacher;
        private readonly IItemCreator _itemCreator;
        private readonly IItemRepository _repository;
        private readonly IUrlGenerator _urlGenerator;

        public ItemsController(
            IItemRepository repository,
            IUrlGenerator urlGenerator,
            IItemCreator itemCreator,
            IItemCacher itemCacher)
        {
            _repository = repository;
            _urlGenerator = urlGenerator;
            _itemCreator = itemCreator;
            _itemCacher = itemCacher;
        }

        public async Task<IHttpActionResult> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();

            return Ok(items);
        }

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            if (!await _itemCacher.ItemExists(id))
                return NotFound();

            var item = await _itemCacher.GetItem(id);

            return Ok(item);
        }

        public async Task<IHttpActionResult> PostAsync(Item item)
        {
            if (!item.IsValidForCreating())
                return BadRequest();

            var newItem = _itemCreator.SetItem(item);

            await _repository.AddAsync(newItem);
            var location = _urlGenerator.GetItemUrl(newItem.Id);

            return Created(location, newItem);
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, Item item)
        {
            var updatedItem = await _repository.UpdateAsync(id, item);

            return Ok(updatedItem);
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
