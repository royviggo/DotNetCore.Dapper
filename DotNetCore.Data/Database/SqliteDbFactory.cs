using System.Data;
using Microsoft.Data.Sqlite;

namespace DotNetCore.Data.Database
{
    public class SqliteDbFactory : DbFactory
    {
        public SqliteDbFactory(string connectionString) : base(connectionString)
        {
        }

        public override IDbConnection Connection(string connectionString)
        {
            return new SqliteConnection(connectionString);
        }
    }
}