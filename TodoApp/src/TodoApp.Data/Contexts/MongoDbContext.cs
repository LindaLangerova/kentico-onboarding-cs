using System;
using System.Data.Entity;
using CodeMash.MongoDB.Repository;
using MongoDB.Driver;
using TodoApp.Contract.Models;

namespace TodoApp.Data.Contexts
{
    public class MongoDbContext
    {
        internal IMongoClient _client { get; set; }
        internal IMongoDatabase _database { get; set; }
        internal IMongoCollection<Item> _itemsCollection { get; set; }

        public MongoDbContext(string mongoDbConnectionString)
        {
            _client = new MongoClient(mongoDbConnectionString);
            _database = _client.GetDatabase("tododb");
            _itemsCollection = _database.GetCollection<Item>("Items");
        }
    }
}