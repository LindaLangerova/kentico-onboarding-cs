using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApp.Contract.Models;
using MongoDB.Driver;

namespace TodoApp.Services.Updaters
{
    public static class ItemUpdater
    {
        public static Task Update(this IMongoCollection<Item> collection, Item item)
        {
            Expression<Func<Item, bool>> filter = i => i.Id == item.Id;
            var options = new FindOneAndUpdateOptions<Item, Item> { ReturnDocument = ReturnDocument.After, IsUpsert = false };

            return collection.ReplaceOneAsync(filter, item, new UpdateOptions());
        }
    }
}
