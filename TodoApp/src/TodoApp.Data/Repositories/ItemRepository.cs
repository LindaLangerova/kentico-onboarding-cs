using System;
using System.Collections.Generic;
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

        public IEnumerable<Item> GetAll()
            => ItemList;

        public Item Get(Guid id)
            => ItemList[0];

        public Item Add(Item item)
            => ItemList[0];

        public Item Update(Guid id, Item item)
            => ItemList[0];

        public void Delete(Guid id) { }
    }
}
