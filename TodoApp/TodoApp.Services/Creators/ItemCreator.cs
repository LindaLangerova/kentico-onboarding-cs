using TodoApp.Contract.Models;
using TodoApp.Contract.Services;

namespace TodoApp.Services.Creators
{
    public class ItemCreator : IItemCreator
    {
        private readonly IDateTimeGenerator _dateTimeGenerator;
        private readonly IIdGenerator _idGenerator;

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
