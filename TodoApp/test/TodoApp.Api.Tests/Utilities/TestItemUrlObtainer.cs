using System;
using TodoApp.Contract;

namespace TodoApp.Api.Tests.Utilities
{
    internal class TestItemUrlObtainer : IItemUrlObtainer
    {
        public string GetItemUrl(Guid id)
        {
            return $"api/v1/itemlist/{id}";
        }
    }
}