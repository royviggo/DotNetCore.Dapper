using System;
using System.Text.RegularExpressions;
using DotNetCore.Data.Enums;
using DotNetCore.Data.Interfaces;

namespace DotNetCore.Data.Models
{
    public class DateStringParser : IDateStringParser
    {
        public GenDate Parse(string dateString)
        {
            var regex = new Regex(@"^(?<stype>\d)(?<fyear>\d{4})(?<fmonth>\d{2})(?<fday>\d{2})(?<dtype>\d)(?<tyear>\d{4})(?<tmonth>\d{2})(?<tday>\d{2})");
            var m = regex.Match(dateString);

            if (m.Success)
            {
                GenDateType dateTypeOut;

                if (Enum.TryParse(m.Groups["dtype"].Value, out dateTypeOut))
                {
                    var fromDate = new DatePart(m.Groups["fyear"].Value, m.Groups["fmonth"].Value, m.Groups["fday"].Value);
                    var toDate = new DatePart(m.Groups["tyear"].Value, m.Groups["tmonth"].Value, m.Groups["tday"].Value);

                    return new GenDate(dateTypeOut, fromDate, toDate, true);
                }
            }
            var datePhrase = dateString.Length > 1 ? dateString.Substring(2, dateString.Length - 1) : "";

            return new GenDate(GenDateType.Invalid, datePhrase, false);
        }

        public DatePart GetDatePartFromStringDate(string sDate)
        {
            var regex = new Regex(@"^(?<year>\d{4})(?<month>\d{2})(?<day>\d{2})");
            var m = regex.Match(sDate);

            return m.Success ? new DatePart(m.Groups["year"].Value, m.Groups["month"].Value, m.Groups["day"].Value)
                : new DatePart();
        }
    }
}