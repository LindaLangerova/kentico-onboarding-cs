using System;
using System.Collections.Generic;
using TodoApp.Contract.Models;

namespace TodoApp.Contract.Repositories
{
    public interface IItemRepository
    {
        List<Item> GetAll();
        Item Get(Guid id);
        Item Update(Guid id, Item item);
        string Add(Item item);
        void Delete(Guid id);
    }
}
