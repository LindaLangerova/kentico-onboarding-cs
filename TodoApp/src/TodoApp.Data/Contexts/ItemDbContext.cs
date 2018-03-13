using System.Data.Entity;
using TodoApp.Contract;
using TodoApp.Contract.Models;

namespace TodoApp.Data.Contexts
{
    public class ItemDbContext : DbContext, IItemDbContext
    {
        public ItemDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<Item> Items { get; set; }
    }
}