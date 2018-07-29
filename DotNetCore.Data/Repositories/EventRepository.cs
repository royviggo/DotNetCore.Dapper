using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Utils;
using GenDateTools;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.IO;
using System.Text;
using System;

namespace DotNetCore.Data.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(IDbFactory dbFactory, IDbTransaction dbTransaction) : base(dbFactory, dbTransaction)
        {
        }

        public override Event Get(int id)
        {
            var sql = GetBaseQuery().Where("e.Id = @Id");

            return Db.Context().Query<EventData, EventType, Place, Event, Event>(sql, (ed, et, p, e) =>
            {
                e.EventType = et;
                e.Place = p;
                e.Date = new GenDate(ed.Date_DateString);
                return e;
            }, param: new { Id = id }, transaction: DbTransaction).FirstOrDefault();
        }

        public IEnumerable<Event> GetByPerson(Person person)
        {
            return GetByPerson(person.Id);
        }

        public IEnumerable<Event> GetByPerson(int personId)
        {
            var query = GetBaseQuery().Where(GetWherePerson());

            return GetListSql(query, new { PersonId = personId });
        }

        public IEnumerable<Event> GetByPerson(IEnumerable<int> personIds)
        {
            var query = GetBaseQuery().Where(GetWherePersonIn());

            return GetListSql(query, new { PersonIds = personIds });
        }

        public IEnumerable<Event> GetByDate(GenDate date)
        {
            var query = GetBaseQuery().Where(GetWhereDate());

            return GetListSql(query, new { date.From, date.To });
        }

        public IEnumerable<Event> GetByEventType(EventType eventType)
        {
            return GetByEventType(eventType.Id);
        }

        public IEnumerable<Event> GetByEventType(int eventTypeId)
        {
            var query = GetBaseQuery().Where(GetWhereEventType());

            return GetListSql(query, new { EventTypeId = eventTypeId });
        }

        public IEnumerable<Event> GetByEventType(IEnumerable<int> eventTypeIds)
        {
            var query = GetBaseQuery().Where(GetWhereEventTypeIn());

            return GetListSql(query, new { EventTypeIds = eventTypeIds });
        }

        public IEnumerable<Event> GetByEventTypeAndDate(int eventTypeId, GenDate date)
        {
            var query = GetBaseQuery().Where(GetWhereEventType()).And(GetWhereDate());

            return GetListSql(query, new { EventTypeId = eventTypeId, date.From, date.To });
        }

        public IEnumerable<Event> GetByEventTypeAndDate(IEnumerable<int> eventTypeIds, GenDate date)
        {
            var query = GetBaseQuery().Where(GetWhereEventTypeIn()).And(GetWhereDate());

            return GetListSql(query, new { EventTypeIds = eventTypeIds, date.From, date.To });
        }

        public IEnumerable<Event> GetByPlace(Place place)
        {
            return GetByPlace(place.Id);
        }

        public IEnumerable<Event> GetByPlace(int placeId)
        {
            var query = GetBaseQuery().Where(GetWherePlace());

            return GetListSql(query, new { PlaceId = placeId });
        }

        public IEnumerable<Event> GetByPlace(IEnumerable<int> placeIds)
        {
            var query = GetBaseQuery().Where(GetWherePlaceIn());

            return GetListSql(query, new { PlaceIds = placeIds });
        }

        public IEnumerable<Event> GetByQuery(IEnumerable<WhereClause> whereList)
        {
            var whereClauses = new WhereClauses(whereList);
            var query = GetBaseQuery().Where(whereClauses.Resolve());

            var result =  GetListSql(query, whereClauses.Parameters);
            return result;
        }

        public override IEnumerable<Event> GetListSql(string query, object param)
        {
            return Db.Context().Query<EventData, EventType, Place, Event, Event>(query, (ed, et, p, e) =>
            {
                e.EventType = et;
                e.Place = p;
                e.Date = new GenDate(ed.Date_DateString);
                return e;
            }, param: param, transaction: DbTransaction);
        }

        public override string GetBaseQuery()
        {
            return @"
                SELECT e.Id, e.EventTypeId, e.PersonId, e.PlaceId,
                       e.Date_DateString,
                       e.Description, e.CreatedDate, e.ModifiedDate,
                       et.Id, et.IsFamilyEvent, et.Name, et.GedcomTag, et.UseDate, et.UsePlace, et.UseDescription, et.Sentence, et.CreatedDate, et.ModifiedDate,
                       p.Id, p.Name, p.CreatedDate, p.ModifiedDate,
                       e.Id, e.EventTypeId, e.PersonId, e.PlaceId, e.Description, e.CreatedDate, e.ModifiedDate
                FROM Events e
                INNER JOIN EventTypes et ON e.EventTypeId = et.Id
                INNER JOIN Places p ON e.PlaceId = p.Id
                ";
        }

        private static string GetWherePerson()
        {
            return @"e.PersonId = @PersonId";
        }

        private static string GetWherePersonIn()
        {
            return @"e.PersonId IN @PersonIds";
        }

        private static string GetWhereDate()
        {
            return @"((e.Date_From BETWEEN @From AND @To) OR 
                      (e.Date_To BETWEEN @From AND @To) OR
                      (@From BETWEEN e.Date_From AND e.Date_To) OR
                      (@To BETWEEN e.Date_From AND e.Date_To))";
        }

        private static string GetWhereEventType()
        {
            return @"(e.EventTypeId = @EventTypeId)";
        }

        private static string GetWhereEventTypeIn()
        {
            return @"(e.EventTypeId IN @EventTypeIds)";
        }

        private static string GetWherePlace()
        {
            return @"(e.PlaceId = @PlaceId)";
        }

        private static string GetWherePlaceIn()
        {
            return @"(e.PlaceId IN @PlaceIds)";
        }
    }
}
