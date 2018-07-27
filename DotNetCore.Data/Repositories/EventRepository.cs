using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Utils;
using GenDateTools;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

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

        public IEnumerable<Event> GetByPersonId(int personId)
        {
            var query = GetBaseQuery().Where("e.PersonId = @PersonId");

            return GetListSql(query, new { PersonId = personId });
        }

        public IEnumerable<Event> GetByDate(GenDate date)
        {
            var query = GetBaseQuery().Where(GetDateWhere());

            return GetListSql(query, new { date.From, date.To });
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

        private static string GetDateWhere()
        {
            return @"((E.Date_From BETWEEN @From AND @To) OR 
                      (E.Date_To BETWEEN @From AND @To) OR
                      (@From BETWEEN E.Date_From AND E.Date_To) OR
                      (@To BETWEEN E.Date_From AND E.Date_To))";
        }
    }
}
