using System;
using System.Data.Entity;
using TodoApp.Contract.Models;

namespace TodoApp.Contract.Contexts
{
    public interface IItemDbContext : IDisposable
    {
        IDbSet<Item> Items { get; set; }
    }
}
