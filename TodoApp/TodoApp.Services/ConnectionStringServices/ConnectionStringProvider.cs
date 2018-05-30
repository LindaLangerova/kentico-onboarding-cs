using System.Configuration;

namespace TodoApp.Services.ConnectionStringServices
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
