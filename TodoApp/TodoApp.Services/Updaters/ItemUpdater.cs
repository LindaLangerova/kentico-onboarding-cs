using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApp.Contract.Models;
using MongoDB.Driver;
using TodoApp.Contract.Services.Updaters;
using TodoApp.Services.Generators;

namespace TodoApp.Services.Updaters
{
    public class ItemUpdater: IItemUpdater
    {
        public async Task<Item> UpdateItemInCollection(IMongoCollection<Item> collection, Guid id , Item item)
        {
            Expression<Func<Item, bool>> filter = i => i.Id == id;
            item.LastChange = DateTimeGenerator.GetActualDateTime();
            await collection.ReplaceOneAsync<Item>(filter, item, new UpdateOptions());

            return item;
        }
    }
}
