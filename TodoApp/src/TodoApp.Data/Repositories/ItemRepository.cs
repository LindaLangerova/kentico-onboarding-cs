using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoApp.Services.ConnectionStringServices;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;

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

        public async Task<List<Item>> GetAllAsync()
            => await _itemsCollection.Find(FilterDefinition<Item>.Empty).ToListAsync();

        public async Task<Item> GetAsync(Guid id)
            => await _itemsCollection.Find(item => item.Id == id).FirstOrDefaultAsync();

        public async Task<Item> AddAsync(Item item)
        {
            await _itemsCollection.InsertOneAsync(item);

            return item;
        }

        public Task<Item> UpdateAsync(Guid id, Item item)
            => throw new NotImplementedException();

        public Task DeleteAsync(Guid id)
            => throw new NotImplementedException();
    }
}
