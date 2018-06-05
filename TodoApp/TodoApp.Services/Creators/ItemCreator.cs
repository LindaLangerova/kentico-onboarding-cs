using System;
using TodoApp.Contract.Models;
using TodoApp.Contract.Services;
using TodoApp.Services.Validators;

namespace TodoApp.Services.Creators
{
    public class ItemCreator : IItemCreator
    {
        private readonly IIdGenerator _idGenerator;

        public ItemCreator(IIdGenerator idGenerator)
            => _idGenerator = idGenerator;

        public Item SetItem(Item item)
        {
            item.Id = _idGenerator.GenerateId();
            item.CreatedAt = DateTime.Now;
            item.LastChange = DateTime.Now;

            return item;
        }
    }
}
