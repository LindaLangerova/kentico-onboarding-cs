using TodoApp.Contract.Models;
using TodoApp.Contract.Services.Creators;
using TodoApp.Contract.Services.Generators;
using TodoApp.Services.Generators;

namespace TodoApp.Services.Creators
{
    internal class ItemCreator : IItemCreator
    {
        private readonly IIdGenerator _idGenerator;

        public ItemCreator(IIdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        public Item SetItem(Item item)
        {
            item.Id = _idGenerator.GenerateId();
            item.CreatedAt = DateTimeGenerator.GetActualDateTime();
            item.LastChange = item.CreatedAt;

            return item;
        }
    }
}
