using System;
using System.Data.Entity;
using TodoApp.Contract.Models;

namespace TodoApp.Contract
{
    public interface IItemDbContext: IDisposable
    {
        IDbSet<ItemModel> Items { get; set; }
    }
}
