using System;
using System.Threading.Tasks;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services.Generators;
using TodoApp.Contract.Services.Providers;
using TodoApp.Contract.Services.Updaters;

namespace TodoApp.Services.Updaters
{
    public class ItemUpdater : IItemUpdater
    {
        private readonly IDateTimeGenerator _dateTimeGenerator;
        private readonly IItemRepository _itemRepository;

        public ItemUpdater(IItemRepository itemRepository, IDateTimeGenerator dateTimeGenerator)
        {
            _itemRepository = itemRepository;
            _dateTimeGenerator = dateTimeGenerator;
        }

        public async Task<Item> UpdateItem(Item item, Item itemWithUpdates)
        {
            item.LastChange = _dateTimeGenerator.GetActualDateTime();
            item.Text = itemWithUpdates.Text;

            await _itemRepository.UpdateAsync(item.Id, item);

            return item;
        }
    }
}
