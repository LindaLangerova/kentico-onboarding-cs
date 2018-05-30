using System;

namespace TodoApp.Services.UrlServices
{
    public interface IUrlGenerator
    {
        string GetItemUrl(Guid id, string routeName);
    }
}
