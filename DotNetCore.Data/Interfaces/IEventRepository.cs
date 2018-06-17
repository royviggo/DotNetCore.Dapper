using DotNetCore.Data.Entities;
using System;
using System.Collections.Generic;

namespace DotNetCore.Data.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>, IDisposable
    {
        IEnumerable<Event> GetByPersonId(int personId);
    }
}
