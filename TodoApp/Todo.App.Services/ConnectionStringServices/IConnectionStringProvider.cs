using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.App.Services.ConnectionStringServices
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString();
    }
}
