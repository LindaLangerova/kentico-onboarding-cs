using System;
using System.Threading.Tasks;
using TodoApp.Contract.Models;

namespace TodoApp.Contract.Services.Providers
{
    public interface IItemCacher
    {
        Task<Item> GetItem(Guid id);
        Task<bool> ItemExists(Guid id);
    }
}
