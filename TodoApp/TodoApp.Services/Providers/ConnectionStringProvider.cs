using System.Configuration;
using TodoApp.Contract.Services;

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
