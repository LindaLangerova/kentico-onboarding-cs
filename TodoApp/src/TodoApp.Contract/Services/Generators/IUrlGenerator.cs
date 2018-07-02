using System;

namespace TodoApp.Contract.Services.Generators
{
    public interface IUrlGenerator
    {
        string GetItemUrl(Guid id);
    }
}
