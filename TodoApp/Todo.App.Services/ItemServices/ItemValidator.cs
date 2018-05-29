using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Contract.Models;

namespace Todo.App.Services.ItemServices
{
    public static class ItemValidator
    {
        public static bool IsValidForCreating(this Item itemFromServer)
        {
            return itemFromServer.IsValidForUpdating() && itemFromServer.Id == Guid.Empty;
        }

        public static bool IsValidForUpdating(this Item itemFromServer)
        {
            return itemFromServer.CreatedAt == default(DateTime) && itemFromServer.LastChange == default(DateTime);
        }

    }
}
