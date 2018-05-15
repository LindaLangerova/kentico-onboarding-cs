using System;
using System.Threading.Tasks;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;

namespace TodoApp.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        internal static Item[] ItemList =
        {
            new Item {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a coffee"},
            new Item {Id = Guid.Parse("55b0d56d-48d7-4f93-bd73-e4b801e26faa"), Text = "Make second coffee"},
            new Item {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d2"), Text = "Add some coffee"},
            new Item {Id = Guid.Parse("250be0cc-438e-46cc-a0fe-549f4d3409e2"), Text = "Coffee overflow"}
        };

        public async Task<Item[]> GetAll()
            => await Task.FromResult(ItemList);

        public async Task<Item> Get(Guid id)
            => await Task.FromResult(ItemList[0]);

        public async Task<Item> Add(Item item)
            => await Task.FromResult(ItemList[0]);

        public async Task<Item> Update(Guid id, Item item)
            => await Task.FromResult(ItemList[0]);

        public void Delete(Guid id) { }
    }
}
