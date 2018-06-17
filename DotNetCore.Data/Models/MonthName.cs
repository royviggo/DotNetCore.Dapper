using System.Collections.Generic;

namespace DotNetCore.Data.Models
{
    public static class MonthName
    {
        public static Dictionary<int, string> GetMonths() => new Dictionary<int, string>
        {
            { 1, "Jan" },
            { 2, "Feb" },
            { 3, "Mar" },
            { 4, "Apr" },
            { 5, "May" },
            { 6, "Jun" },
            { 7, "Jul" },
            { 8, "Aug" },
            { 9, "Sep" },
            {10, "Oct" },
            {11, "Nov" },
            {12, "Dec" },
        };

        public static string ToMonthName(this int month)
        {
            return GetMonths().ContainsKey(month) ? GetMonths()[month] : "";
        }

    }
}