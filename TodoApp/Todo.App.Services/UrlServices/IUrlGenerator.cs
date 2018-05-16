using System;

namespace Todo.App.Services.UrlServices
{
    public interface IUrlGenerator
    {
        string GetItemUrl(Guid id, string routeName);
    }
}
