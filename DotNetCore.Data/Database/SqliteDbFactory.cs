using System.Data;
using DotNetCore.Data.Interfaces;
using Microsoft.Data.Sqlite;

namespace DotNetCore.Data.Database
{
    public class SqliteDbFactory : IDbFactory
    {
        private string _connectionString;
        private IDbConnection _dbContext;

        public SqliteDbFactory()
        {
        }

        public SqliteDbFactory(string connectionString)
        {
            _connectionString = connectionString;
            _dbContext = new SqliteConnection(connectionString);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            _dbContext = null;
        }

        public IDbConnection Context()
        {
            return _dbContext ?? (_dbContext = new SqliteConnection(_connectionString));
        }
    }
}