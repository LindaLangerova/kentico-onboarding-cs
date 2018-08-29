using System;
using System.Threading.Tasks;
using TodoApp.Contract.Models;

namespace TodoApp.Contract.Services.Updaters
{
    public interface IItemUpdater
    {
        Task<Item> UpdateItem(Item item, Item itemWithUpdates);
    }
}
