using TodoApp.Contract.Models;
using TodoApp.Contract.Services.Creators;
using TodoApp.Contract.Services.Generators;
using TodoApp.Services.Generators;

namespace TodoApp.Services.Creators
{
    internal class ItemCreator : IItemCreator
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IDateTimeGenerator _dateTimeGenerator;

        public ItemCreator(IIdGenerator idGenerator, IDateTimeGenerator dateTimeGenerator)
        {
            _idGenerator = idGenerator;
            _dateTimeGenerator = dateTimeGenerator;
        }

        public Item SetItem(Item item)
        {
            item.Id = _idGenerator.GenerateId();
            item.CreatedAt = _dateTimeGenerator.GetActualDateTime();
            item.LastChange = item.CreatedAt;

            return item;
        }
    }
}
