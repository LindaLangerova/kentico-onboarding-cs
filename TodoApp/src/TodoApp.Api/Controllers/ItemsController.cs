﻿using System;
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
    [EnableCors("*", "*", "*"), ApiVersion("1.0")]
    public class ItemsController : ApiController
    {
        private readonly IItemRepository _repository;
        private readonly IUrlGenerator _urlGenerator;
        private readonly IItemCreator _itemCreator;

        public ItemsController(IItemRepository repository, IUrlGenerator urlGenerator, IItemCreator itemCreator)
        {
            _repository = repository;
            _urlGenerator = urlGenerator;
            _itemCreator = itemCreator;
        }

        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await _repository.GetAll();
            if (result == null) return NotFound();
            return Ok(result);
        }

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            var item = await _repository.Get(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        public async Task<IHttpActionResult> PostAsync(Item item)
        {
            if (item?.Text == null)
                return BadRequest();

            item = _itemCreator.SetItem(item.Text);

            var newItemId = await _repository.Add(item);
            var location = _urlGenerator.GetItemUrl(newItemId, RouteConfig.DefaultApi);
            return Created(location, newItemId);
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, Item item)
        {
            var updatedItem = await _repository.Update(id, item);
            return Ok(updatedItem);
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
