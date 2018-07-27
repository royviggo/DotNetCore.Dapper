using DotNetCore.Data.Entities;
using GenDateTools;
using System;
using System.Collections.Generic;

namespace DotNetCore.Data.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>, IDisposable
    {
        IEnumerable<Event> GetByPersonId(int personId);
        IEnumerable<Event> GetByDate(GenDate date);
    }
}
