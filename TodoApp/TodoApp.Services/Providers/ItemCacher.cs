using System;
using System.Threading.Tasks;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services;

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
            if (_actualItem != null && _actualItem.Id == id)
                return _actualItem;

            _actualItem = await _repository.GetAsync(id);

            return _actualItem;
        }

        public async Task<bool> ItemExists(Guid id)
        {
            if (_actualItem != null && _actualItem.Id == id)
                return true;

            try
            {
                _actualItem = await _repository.GetAsync(id);
            } catch (NullReferenceException)
            {
                return false;
            }

            return _actualItem != null;
        }
    }
}
