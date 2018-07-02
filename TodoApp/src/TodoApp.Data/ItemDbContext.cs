using System.Data.Entity;
using TodoApp.Contract;
using TodoApp.Contract.Models;

namespace TodoApp.Data
{
    public class ItemDbContext : DbContext, IItemDbContext
    {
        public ItemDbContext(string connectionString)
            : base(connectionString)
        {
        }
        public IDbSet<ItemModel> Items { get; set;}
    }
}
