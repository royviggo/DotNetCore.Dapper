namespace DotNetCore.Data.Database
{
    public enum DataBaseType
    {
        MySQL,
        PostgreSQL,
        SQLite,
        SQLServer,
   }

    public static class DatabaseUtil
    {
        public static string IdentitySql(DataBaseType dbType)
        {
            switch (dbType)
            {
                case DataBaseType.MySQL:
                    return "SELECT LAST_INSERT_ID() AS id";
                case DataBaseType.PostgreSQL:
                    return "SELECT LASTVAL() AS id";
                case DataBaseType.SQLite:
                    return "SELECT LAST_INSERT_ROWID() AS id";
                case DataBaseType.SQLServer:
                    return "SELECT CAST(SCOPE_IDENTITY() AS BIGINT) AS [id]";
                default:
                    return "";
            }
        }

        public static string PagedSqlTemplate(DataBaseType dbType, int rowsPerPage, int pageNumber)
        {
            var offset = ((pageNumber - 1) * rowsPerPage) + 1;
            switch (dbType)
            {
                case DataBaseType.MySQL:
                    return $"{0} LIMIT {offset}, {rowsPerPage}";
                case DataBaseType.PostgreSQL:
                    return $"{0} LIMIT {rowsPerPage} OFFSET {offset}";
                case DataBaseType.SQLite:
                    return $"{0} LIMIT {rowsPerPage} OFFSET {offset}";
                case DataBaseType.SQLServer:
                    var lastRow = pageNumber * rowsPerPage;
                    return $"SELECT * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY null) AS RowNumber FROM ({0}) AS A) AS B WHERE RowNumber BETWEEN {offset} AND {lastRow}";
                default:
                    return "";
            }
        }

        public static string EncapsulationTemplate(DataBaseType dbType)
        {
            switch (dbType)
            {
                case DataBaseType.MySQL:
                    return "`{0}`";
                case DataBaseType.PostgreSQL:
                    return "\"{0}\"";
                case DataBaseType.SQLite:
                    return "\"{0}\"";
                case DataBaseType.SQLServer:
                    return "[{0}]";
                default:
                    return "{0}";
            }
        }

    }
}
