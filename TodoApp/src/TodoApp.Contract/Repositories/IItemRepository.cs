using System;
using System.Threading.Tasks;
using TodoApp.Contract.Models;

namespace TodoApp.Contract.Repositories
{
    public interface IItemRepository
    {
        Task<Item[]> GetAllAsync();
        Task<Item> GetAsync(Guid id);
        Task<Item> UpdateAsync(Guid id, Item item);
        Task<Item> AddAsync(Item item);
        void Delete(Guid id);
    }
}
