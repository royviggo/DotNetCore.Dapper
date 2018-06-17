using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Dapper;
using DotNetCore.Data.Models;

namespace DotNetCore.Data.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(IDbFactory dbFactory, IDbTransaction dbTransaction) : base(dbFactory, dbTransaction)
        {
        }

        public override Event Get(int id)
        {
            var sql = GetBaseEventSql() + "WHERE e.Id = @Id";

            return Db.Context().Query<EventData, EventType, Place, Event, Event>(sql, (ed, et, p, e) =>
            {
                e.EventType = et;
                e.Place = p;
                e.Date = new GenDate(ed.Date_DateType, new DatePart(ed.Date_DateFrom_Year, ed.Date_DateFrom_Month, ed.Date_DateFrom_Day),
                                     new DatePart(ed.Date_DateTo_Year, ed.Date_DateTo_Month, ed.Date_DateTo_Day), ed.Date_DatePhrase, ed.Date_IsValid);
                return e;
            }, param: new { Id = id }, transaction: DbTransaction).FirstOrDefault();
        }

        public IEnumerable<Event> GetByPersonId(int personId)
        {
            var sql = GetBaseEventSql() + "WHERE e.PersonId = @PersonId";

            return Db.Context().Query<EventData, EventType, Place, Event, Event>(sql, (ed, et, p, e) =>
            {
                e.EventType = et;
                e.Place = p;
                e.Date = new GenDate(ed.Date_DateType, new DatePart(ed.Date_DateFrom_Year, ed.Date_DateFrom_Month, ed.Date_DateFrom_Day),
                                     new DatePart(ed.Date_DateTo_Year, ed.Date_DateTo_Month, ed.Date_DateTo_Day), ed.Date_DatePhrase, ed.Date_IsValid);
                return e;
            }, param: new { PersonId = personId }, transaction: DbTransaction);
        }

        private string GetBaseEventSql()
        {
            return @"
                SELECT e.Id, e.EventTypeId, e.PersonId, e.PlaceId,
                       e.Date_DateType, 
                       e.Date_DateFrom_Year, e.Date_DateFrom_Month, e.Date_DateFrom_Day, e.Date_DateTo_Year, e.Date_DateTo_Month, e.Date_DateTo_Day, 
                       e.Date_DatePhrase, e.Date_SortDate, e.Date_IsValid,
                       e.Description, e.CreatedDate, e.ModifiedDate,
                       et.Id, et.IsFamilyEvent, et.Name, et.GedcomTag, et.UseDate, et.UsePlace, et.UseDescription, et.Sentence, et.CreatedDate, et.ModifiedDate,
                       p.Id, p.Name, p.CreatedDate, p.ModifiedDate,
                       e.Id, e.EventTypeId, e.PersonId, e.PlaceId, e.Description, e.CreatedDate, e.ModifiedDate
                FROM Events e
                INNER JOIN EventTypes et ON e.EventTypeId = et.Id
                LEFT OUTER JOIN Places p ON e.PlaceId = p.Id
                ";
        }
    }
}
