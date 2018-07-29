using DotNetCore.Data.Entities;
using DotNetCore.Data.Utils;
using GenDateTools;
using System;
using System.Collections.Generic;

namespace DotNetCore.Data.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>, IDisposable
    {
        IEnumerable<Event> GetByPerson(Person person);
        IEnumerable<Event> GetByPerson(int personId);
        IEnumerable<Event> GetByPerson(IEnumerable<int> personIds);

        IEnumerable<Event> GetByDate(GenDate date);

        IEnumerable<Event> GetByEventType(EventType eventType);
        IEnumerable<Event> GetByEventType(int eventTypeId);
        IEnumerable<Event> GetByEventType(IEnumerable<int> eventTypeIds);

        IEnumerable<Event> GetByEventTypeAndDate(int eventTypeId, GenDate date);
        IEnumerable<Event> GetByEventTypeAndDate(IEnumerable<int> eventTypeIds, GenDate date);

        IEnumerable<Event> GetByPlace(Place place);
        IEnumerable<Event> GetByPlace(int placeId);
        IEnumerable<Event> GetByPlace(IEnumerable<int> placeIds);

        IEnumerable<Event> GetByQuery(IEnumerable<WhereClause> whereList);
    }
}
