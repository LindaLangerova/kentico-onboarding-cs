using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Web.Http;
using Todo.App.Services.ItemServices;
using Todo.App.Services.UrlServices;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;

namespace TodoApp.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [ApiVersion("1.0")]
    public class ItemsController : ApiController
    {
        private readonly IItemCreator _itemCreator;
        private readonly IItemRepository _repository;
        private readonly IUrlGenerator _urlGenerator;
        private readonly IItemCacher _itemCacher;

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
            if (!_itemCreator.SetItem(ref item))
                return BadRequest();

            if (await _itemCacher.ItemExists(item.Id))
                return BadRequest();

            var newItem = await _repository.AddAsync(item);
            var location = _urlGenerator.GetItemUrl(newItem.Id, RouteConfig.DefaultApi);

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
