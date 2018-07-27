using DotNetCore.Data.Entities;
using GenDateTools;
using System;
using System.Collections.Generic;

namespace DotNetCore.Data.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>, IDisposable
    {
        IEnumerable<Event> GetByPerson(int personId);
        IEnumerable<Event> GetByDate(GenDate date);
        IEnumerable<Event> GetByEventType(EventType eventType);
        IEnumerable<Event> GetByEventType(int eventTypeId);
        IEnumerable<Event> GetByEventTypeAndDate(int eventTypeId, GenDate date);
        IEnumerable<Event> GetByPlace(Place place);
        IEnumerable<Event> GetByPlace(int placeId);
    }
}
