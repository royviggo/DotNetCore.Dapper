using System.Collections.Generic;
using DotNetCore.Data.Enums;
using Dapper.Contrib.Extensions;

namespace DotNetCore.Data.Entities
{
    public class Person : Entity
    {
        public string FirstName { get; set; }

        public string FatherName { get; set; }

        public string Patronym { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public int? BornYear { get; set; }

        public int? DeathYear { get; set; }

        public Status Status { get; set; }

        [Computed]
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
