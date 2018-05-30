using System;
using Todo.App.Services.IdServices;
using TodoApp.Contract.Models;

namespace Todo.App.Services.ItemServices
{
    public class ItemCreator : IItemCreator
    {
        private readonly IdGenerator _idGenerator;

        public ItemCreator(IdGenerator idGenerator)
            => _idGenerator = idGenerator;

        public bool SetItem(ref Item item)
        {
            if (!item.IsValidForCreating())
                return false;
            
            item.Id = _idGenerator.GenerateId();
            item.Text = item.Text;
            item.CreatedAt = DateTime.Now;
            item.LastChange = DateTime.Now;

            return true;
        }
    }
}
