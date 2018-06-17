using Dapper.Contrib.Extensions;
using DotNetCore.Data.Enums;

namespace DotNetCore.Data.Entities
{
    [Table("Events")]
    public class EventData : Entity
    {
        public int EventTypeId { get; set; }

        public int PersonId { get; set; }

        public int PlaceId { get; set; }

        public GenDateType Date_DateType { get; set; }
        public int Date_DateFrom_Year { get; set; }
        public int Date_DateFrom_Month { get; set; }
        public int Date_DateFrom_Day { get; set; }
        public int Date_DateTo_Year { get; set; }
        public int Date_DateTo_Month { get; set; }
        public int Date_DateTo_Day { get; set; }
        public string Date_DatePhrase { get; set; }
        public int Date_SortDate { get; set; }
        public bool Date_IsValid { get; set; }

        public string Description { get; set; }
    }
}
