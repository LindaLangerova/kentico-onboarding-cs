using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using MongoDB.Bson;
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

        public async Task<List<Item>> GetAll() => await _itemsCollection.Find(FilterDefinition<Item>.Empty).ToListAsync();

        public async Task<Item> Get(Guid id) => await _itemsCollection.Find(item => item.Id == id).FirstOrDefaultAsync();

        public async Task<string> Add(Item item)
        {
            await _itemsCollection.InsertOneAsync(item);
            return  item?.Text;
        }

        public Task<Item> Update(Guid id, Item item) => throw new NotImplementedException();

        public Task Delete(Guid id) => throw new NotImplementedException();
    }
}
