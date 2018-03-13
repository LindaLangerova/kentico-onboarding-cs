using System;

namespace TodoApp.Contract
{
    public interface IItemUrlObtainer
    {
        string GetItemUrl(Guid id);
    }
}
