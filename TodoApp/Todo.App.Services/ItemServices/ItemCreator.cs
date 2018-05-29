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

        public Item SetItem(Item item)
            => item.IsValidForCreating()
                   ? new Item {Id = _idGenerator.GenerateId(), Text = item.Text, CreatedAt = DateTime.Now, LastChange = DateTime.Now}
                   : throw new ArgumentException();
    }
}
