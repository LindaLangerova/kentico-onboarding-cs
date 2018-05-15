using System;
using System.Threading.Tasks;
using TodoApp.Contract.Models;

namespace TodoApp.Contract.Repositories
{
    public interface IItemRepository
    {
        Task<Item[]> GetAll();
        Task<Item> Get(Guid id);
        Task<Item> Update(Guid id, Item item);
        Task<Item> Add(Item item);
        void Delete(Guid id);
    }
}
