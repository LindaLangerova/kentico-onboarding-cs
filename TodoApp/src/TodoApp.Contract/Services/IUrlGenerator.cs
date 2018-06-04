using System;

namespace TodoApp.Contract.Services
{
    public interface IUrlGenerator
    {
        string GetItemUrl(Guid id, string routeName);
    }
}
