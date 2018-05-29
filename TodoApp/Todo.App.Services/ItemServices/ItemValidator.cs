using System;
using TodoApp.Contract.Models;

namespace Todo.App.Services.ItemServices
{
    public static class ItemValidator
    {
        public static bool IsValidForCreating(this Item itemFromServer)
            => itemFromServer.IsValidForUpdating() && itemFromServer.Id == Guid.Empty;

        public static bool IsValidForUpdating(this Item itemFromServer)
            => itemFromServer.CreatedAt == default(DateTime) && itemFromServer.LastChange == default(DateTime);
    }
}
