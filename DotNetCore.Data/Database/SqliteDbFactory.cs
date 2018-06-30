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

        public override string IdentitySql()
        {
            return "SELECT LAST_INSERT_ROWID() AS id";
        }

        public override string PagingTemplate(int pageNumber, int rowsPerPage)
        {
            var offset = ((pageNumber - 1) * rowsPerPage) + 1;
            return $"{{0}} LIMIT {rowsPerPage} OFFSET {offset}";
        }

        public override string EncapsulationTemplate()
        {
            return "\"{0}\"";
        }
    }
}