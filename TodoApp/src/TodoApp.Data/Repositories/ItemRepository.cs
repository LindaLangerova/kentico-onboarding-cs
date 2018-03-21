using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using TodoApp.Contract.Models;
using TodoApp.Contract.Repositories;
using TodoApp.Data.Contexts;

namespace TodoApp.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly MongoDbContext _db;

        public ItemRepository(MongoDbContext db) => _db = db;

        public List<Item> GetAll() => _db.ItemsCollection.Find(new BsonDocument()).ToListAsync().Result;

        public Item Get(Guid id) => _db.ItemsCollection.Find(item => item.Id == id).First();

        public string Add(Item item)
        {
            _db.ItemsCollection.InsertOneAsync(item).Wait();
            return item.Text;
        }

        public Item Update(Guid id, Item item) => throw new NotImplementedException();

        public void Delete(Guid id) => throw new NotImplementedException();
    }
}
