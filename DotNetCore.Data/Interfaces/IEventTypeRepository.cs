using DotNetCore.Data.Entities;
using System;

namespace DotNetCore.Data.Interfaces
{
    public interface IEventTypeRepository : IGenericRepository<EventType>, IDisposable
    {
    }
}
