using System;

namespace TodoApp.Contract
{
    public interface IItemUrlManager
    {
        string GetItemUrl(Guid id);
    }
}