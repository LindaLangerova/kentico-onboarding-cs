using System;
using System.Collections.Generic;
using TodoApp.Contract.Models;

namespace TodoApp.Data.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<ItemModel> GetAll();
        ItemModel Get(Guid id);
        ItemModel Update(Guid id, ItemModel item);
        ItemModel Add(ItemModel item);
        void Delete(Guid id);
    }
}
