using DotNetCore.Data.Models;

namespace DotNetCore.Data.Interfaces
{
    public interface IDateStringParser
    {
        GenDate Parse(string dateString);
        DatePart GetDatePartFromStringDate(string sDate);
    }
}