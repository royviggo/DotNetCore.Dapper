using System;

namespace DotNetCore.Data.Utils
{
    public static class DatabaseExtensions
    {
        public static string Select(this Type type)
        {
            return $"SELECT * FROM {type.Name}s ";
        }

        public static string Where(this string query, string where)
        {
            return !string.IsNullOrWhiteSpace(where) ? $"{query} WHERE {where} " : query;
        }

        public static string And(this string query, string where)
        {
            return !string.IsNullOrWhiteSpace(where) ? $"{query} AND {where} " : query;
        }

        public static string OrderBy(this string query, string orderBy)
        {
            return !string.IsNullOrWhiteSpace(orderBy) ? $"{query} ORDER BY {orderBy} " : query;
        }

        public static string Paging(this string query, string template)
        {
            return string.Format(template, query);
        }
    }
}
