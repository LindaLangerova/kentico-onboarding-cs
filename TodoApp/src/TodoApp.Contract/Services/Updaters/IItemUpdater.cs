using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoApp.Contract.Models;

namespace TodoApp.Contract.Services.Updaters
{
    public interface IItemUpdater
    {
        Task<Item> UpdateItemInCollection(IMongoCollection<Item> collection, Guid id, Item item);
    }
}
