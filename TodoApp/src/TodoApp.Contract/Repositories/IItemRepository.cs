using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Contract.Models;

namespace TodoApp.Contract.Repositories
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllAsync();
        Task<Item> GetAsync(Guid id);
        Task<Item> UpdateAsync(Guid id, Item item);
        Task<Guid> AddAsync(Item item);
        Task DeleteAsync(Guid id);
    }
}
