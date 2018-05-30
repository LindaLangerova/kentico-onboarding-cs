using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Contract.Models;

namespace Todo.App.Services.ItemServices
{
    public interface IItemCacher
    {
        Task<Item> GetItem(Guid id);
        Task<bool> ItemExists(Guid id);
    }
}
