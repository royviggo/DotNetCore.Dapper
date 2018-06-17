using DotNetCore.Data.Interfaces;
using System;

namespace DotNetCore.Data.Entities
{
    public class Entity : IEntity
    {
        public void Dispose() { }

        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
