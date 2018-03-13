using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Contract.Models;

namespace TodoApp.Contract.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAll();
        Item Get(Guid id);
        Item Update(Guid id, Item item);
        Item Add(Item item);
        void Delete(Guid id);
    }
}
