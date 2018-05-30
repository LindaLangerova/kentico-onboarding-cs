using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.App.Services.ConnectionStringServices
{
    public class ConnectionStringProvider: IConnectionStringProvider
    {
        private readonly string _connectionString;

        public ConnectionStringProvider()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
