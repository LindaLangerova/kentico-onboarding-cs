using System;
using System.Collections.Generic;
using TodoApp.Contract.Models;

namespace TodoApp.Data.Repositories
{
    public class ItemRepository:IItemRepository
    {
        public static ItemModel[] ItemList =
        {
            new ItemModel {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make a coffee"},
            new ItemModel {Id = Guid.Parse("55b0d56d-48d7-4f93-bd73-e4b801e26faa"), Text = "Make second coffee"},
            new ItemModel {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d2"), Text = "Add some coffee"},
            new ItemModel {Id = Guid.Parse("250be0cc-438e-46cc-a0fe-549f4d3409e2"), Text = "Coffee overflow"}
        };
        
        public IEnumerable<ItemModel> GetAll()
        {
            return ItemList;
        }

        public ItemModel Get(Guid id)
        {
            return ItemList[0];
        }
        
        public ItemModel Add(ItemModel item)
        {
            return ItemList[0];
        }
        
        public ItemModel Update(Guid id, ItemModel item)
        {
            return ItemList[0];
        }

        public void Delete(Guid id)
        {
        }
    }
}
