using Dapper.Contrib.Extensions;

namespace DotNetCore.Data.Entities
{
    [Table("Events")]
    public class EventData : Entity
    {
        public int EventTypeId { get; set; }

        public int PersonId { get; set; }

        public int PlaceId { get; set; }

        public long Date_DateString { get; set; }
        public int Date_DateFrom { get; set; }
        public int Date_DateTo { get; set; }
        public string Date_DatePhrase { get; set; }
        public int Date_SortDate { get; set; }
        public bool Date_IsValid { get; set; }

        public string Description { get; set; }
    }
}
