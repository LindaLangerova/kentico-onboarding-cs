using System;
using System.Threading.Tasks;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services;
using TodoApp.Contract.Services.Providers;

namespace TodoApp.Services.Providers
{
    public class ItemCacher : IItemCacher
    {
        private readonly IItemRepository _repository;
        private Item _actualItem;

        public ItemCacher(IItemRepository repository)
        {
            _repository = repository;
            _actualItem = null;
        }

        public async Task<Item> GetItem(Guid id)
        {
            _actualItem = _actualItem?.Id == id ? _actualItem : await _repository.GetAsync(id);

            return _actualItem;
        }

        public async Task<bool> ItemExists(Guid id)
        {
            if (_actualItem != null && _actualItem.Id == id)
                return true;

            _actualItem = await _repository.GetAsync(id);

            return _actualItem != null;
        }
    }
}
