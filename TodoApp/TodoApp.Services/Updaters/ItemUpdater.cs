using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApp.Contract.Models;
using MongoDB.Driver;
using TodoApp.Contract.Services.Generators;
using TodoApp.Services.Generators;

namespace TodoApp.Services.Updaters
{
    public static class ItemUpdater
    {
        public static Task<Item> Update(this IMongoCollection<Item> collection, Guid id , Item item)
        {
            Expression<Func<Item, bool>> filter = i => i.Id == id;
            item.LastChange = DateTimeGenerator.GetActualDateTime();
            collection.ReplaceOneAsync<Item>(filter, item, new UpdateOptions());

            return Task.FromResult(item);
        }
    }
}
