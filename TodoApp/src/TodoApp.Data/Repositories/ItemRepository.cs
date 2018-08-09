using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Contract.Services.Providers;
using TodoApp.Services.Updators;

namespace TodoApp.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IMongoCollection<Item> _itemsCollection;

        public ItemRepository(IConnectionStringProvider connectionStringProvider)
        {
            var databaseUrl = MongoUrl.Create(connectionStringProvider.GetConnectionString());
            var database = new MongoClient(databaseUrl).GetDatabase(databaseUrl.DatabaseName);
            _itemsCollection = database.GetCollection<Item>("Items");
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
            => await _itemsCollection.Find(FilterDefinition<Item>.Empty).ToListAsync();

        public async Task<Item> GetAsync(Guid id)
            => await _itemsCollection.Find(item => item.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(Item item)
            => await _itemsCollection.InsertOneAsync(item);

        public async Task<Item> UpdateAsync(Guid id, Item item, DateTime actualDateTime)
        {
            Expression<Func<Item, bool>> filter = i => i.Id == id;
            var update = Builders<Item>.Update.Set("Text", $"{item.Text}").Set("LastChange", $"{actualDateTime}");
            var options = new FindOneAndUpdateOptions<Item, Item> {ReturnDocument = ReturnDocument.After, IsUpsert = false};

            //return await _itemsCollection.FindOneAndUpdateAsync(filter, update, options);
            return await _itemsCollection.Update(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            Expression<Func<Item, bool>> filter = i => i.Id == id;
            await _itemsCollection.FindOneAndDeleteAsync(filter);
        }
    }
}
