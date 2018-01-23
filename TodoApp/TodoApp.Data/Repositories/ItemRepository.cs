using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Contract.Models;

namespace TodoApp.Data.Repositories
{
    class ItemRepository:IItemRepository
    {
        public IEnumerable<ItemModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ItemModel Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ItemModel Update(Guid id, ItemModel item)
        {
            throw new NotImplementedException();
        }

        public ItemModel Add(ItemModel item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
