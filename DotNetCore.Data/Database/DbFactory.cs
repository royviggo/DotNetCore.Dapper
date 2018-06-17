using System.Data;
using DotNetCore.Data.Interfaces;
using Microsoft.Data.Sqlite;

namespace DotNetCore.Data.Database
{
    public class DbFactory : IDbFactory
    {
        private string _connectionString;
        private IDbConnection _dbContext;

        public DbFactory()
        {
        }

        public DbFactory(string connectionString)
        {
            _connectionString = connectionString;
            _dbContext = new SqliteConnection(connectionString);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public IDbConnection Context()
        {
            return _dbContext ?? (_dbContext = new SqliteConnection(_connectionString));
        }
    }
}