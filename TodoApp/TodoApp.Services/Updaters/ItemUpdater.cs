using System;
using System.Threading.Tasks;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services.Generators;
using TodoApp.Contract.Services.Updaters;

namespace TodoApp.Services.Updaters
{
    public class ItemUpdater : IItemUpdater
    {
        private readonly IItemRepository _itemRepository;
        private readonly IDateTimeGenerator _dateTimeGenerator;

        public ItemUpdater(IItemRepository itemRepository, IDateTimeGenerator dateTimeGenerator)
        {
            _itemRepository = itemRepository;
            _dateTimeGenerator = dateTimeGenerator;
        }

        public async Task<Item> UpdateItem(Guid id, Item item)
        {
            item.LastChange = _dateTimeGenerator.GetActualDateTime();
            await _itemRepository.UpdateAsync(id, item);
            return item;
        }
    }
}
