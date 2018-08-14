using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoApp.Contract.Models;
using TodoApp.Contract.Services.Generators;
using TodoApp.Contract.Services.Updaters;

namespace TodoApp.Services.Updaters
{
    public class ItemUpdater : IItemUpdater
    {
        private readonly IDateTimeGenerator _dateTimeGenerator;

        public ItemUpdater(IDateTimeGenerator dateTimeGenerator)
            => _dateTimeGenerator = dateTimeGenerator;

        public async Task<Item> UpdateItemInCollection(IMongoCollection<Item> collection, Guid id, Item item)
        {
            Expression<Func<Item, bool>> filter = i => i.Id == id;
            item.LastChange = _dateTimeGenerator.GetActualDateTime();
            await collection.ReplaceOneAsync<Item>(filter, item, new UpdateOptions());

            return item;
        }
    }
}
