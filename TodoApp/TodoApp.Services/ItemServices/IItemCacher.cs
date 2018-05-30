using System;
using System.Threading.Tasks;
using TodoApp.Contract.Models;

namespace TodoApp.Services.ItemServices
{
    public interface IItemCacher
    {
        Task<Item> GetItem(Guid id);
        Task<bool> ItemExists(Guid id);
    }
}
