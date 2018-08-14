using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Contract.Models;

namespace TodoApp.Contract.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item> GetAsync(Guid id);
        Task<Item> UpdateAsync(Guid id, Item item);
        Task AddAsync(Item item);
        Task DeleteAsync(Guid id);
    }
}
