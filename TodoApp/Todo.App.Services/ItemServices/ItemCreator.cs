using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.App.Services.IdServices;
using TodoApp.Contract.Models;

namespace Todo.App.Services.ItemServices
{
    public class ItemCreator : IItemCreator
    {
        private readonly IdGenerator _idGenerator;

        public ItemCreator(IdGenerator idGenerator) => _idGenerator = idGenerator;

        public Item SetItem(string text) => new Item
        {
            Id = _idGenerator.GenerateId()
        };
    }
}
