using MongoDB.Driver;
using TodoApp.Contract.Models;

namespace TodoApp.Data.Contexts
{
    public class MongoDbContext
    {
        public MongoDbContext(string mongoDbConnectionString)
        {
            Client = new MongoClient(mongoDbConnectionString);
            Database = Client.GetDatabase("tododb");
            ItemsCollection = Database.GetCollection<Item>("Items");
        }

        internal IMongoClient Client { get; set; }
        internal IMongoDatabase Database { get; set; }
        internal IMongoCollection<Item> ItemsCollection { get; set; }
    }
}
