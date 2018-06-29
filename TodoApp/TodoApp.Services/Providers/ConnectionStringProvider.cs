using System.Configuration;
using TodoApp.Contract.Services;
using TodoApp.Contract.Services.Providers;

namespace TodoApp.Services.Providers
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly string _connectionString;

        public ConnectionStringProvider()
            => _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public string GetConnectionString()
            => _connectionString;
    }
}
