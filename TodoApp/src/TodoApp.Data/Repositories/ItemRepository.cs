using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;

namespace TodoApp.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IMongoCollection<Item> _itemsCollection;

        public ItemRepository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("tododb");
            _itemsCollection = database.GetCollection<Item>("Items");
        }

        public async Task<List<Item>> GetAllAsync()
            => await _itemsCollection.Find(FilterDefinition<Item>.Empty).ToListAsync();

        public async Task<Item> GetAsync(Guid id)
            => await _itemsCollection.Find(item => item.Id == id).FirstOrDefaultAsync();

        public async Task<Guid> AddAsync(Item item)
        {
            await _itemsCollection.InsertOneAsync(item);

            return item.Id;
        }

        public Task<Item> UpdateAsync(Guid id, Item item)
            => throw new NotImplementedException();

        public Task DeleteAsync(Guid id)
            => throw new NotImplementedException();
    }
}
