using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Contract.Models;

namespace TodoApp.Contract.Repositories
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAll();
        Task<Item> Get(Guid id);
        Task<Item> Update(Guid id, Item item);
        Task<string> Add(Item item);
        Task Delete(Guid id);
    }
}
