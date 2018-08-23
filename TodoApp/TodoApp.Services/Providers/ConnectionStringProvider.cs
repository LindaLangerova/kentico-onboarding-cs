using System.Configuration;
using TodoApp.Contract.Services.Providers;

namespace TodoApp.Services.Providers
{
    internal class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly string _connectionString;

        public ConnectionStringProvider()
            => _connectionString = ConfigurationManager.ConnectionStrings["mongo_list_connection"].ConnectionString;

        public string GetConnectionString()
            => _connectionString;
    }
}
