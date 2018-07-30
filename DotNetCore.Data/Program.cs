using System;
using System.Collections.Generic;
using System.IO;
using DotNetCore.Data.Database;
using DotNetCore.Data.Utils;
using GenDateTools;

namespace DotNetCore
{
    public class Program
    {
        public static void Main()
        {
            var connectionString = "Data Source=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "DotNetCore.sqlite");
            var dbFactory = new SqliteDbFactory(connectionString);

            using (var unitOfWork = new UnitOfWork(dbFactory))
            {

                Console.WriteLine("Places");

                var places = unitOfWork.Places.GetAll();
                foreach (var p in places)
                {
                    Console.WriteLine("{0} - {1}", p.Id, p.Name);
                }

                Console.WriteLine();
                Console.WriteLine("Events for a person");

                var events = unitOfWork.Events.GetByPerson(1);
                foreach (var e in events)
                {
                    Console.WriteLine("{0} - {1} {2}, {3} - {4}", e.Id, e.EventType.Name, e.Date, e.Place?.Name, e.Description);
                }

                Console.WriteLine();
                Console.WriteLine("All events");

                var eventsAll = unitOfWork.Events.GetList("");
                foreach (var e in eventsAll)
                {
                    Console.WriteLine("{0} - {1} {2}, {3} - {4}", e.Id, e.EventType.Name, e.Date, e.Place?.Name, e.Description);
                }

                Console.WriteLine();
                Console.WriteLine("Events between 1700 and 1800");

                var eventsBetween = unitOfWork.Events.GetByDate(new GenDate(GenDateType.Between, new DatePart(17000000), new DatePart(17990000)));
                foreach (var e in eventsBetween)
                {
                    Console.WriteLine("{0} - {1} {2}, {3} - {4}", e.Id, e.EventType.Name, e.Date, e.Place?.Name, e.Description);
                }

                Console.WriteLine();
                Console.WriteLine("Events of type birth");

                var eventsType = unitOfWork.Events.GetByEventType(1);
                foreach (var e in eventsType)
                {
                    Console.WriteLine("{0} - {1} {2}, {3} - {4}", e.Id, e.EventType.Name, e.Date, e.Place?.Name, e.Description);
                }

                Console.WriteLine();
                Console.WriteLine("Beginning of life Events");

                var eventsBol = unitOfWork.Events.GetByEventType(new int[] { 1, 3, 7 });
                foreach (var e in eventsBol)
                {
                    Console.WriteLine("{0} - {1} {2}, {3} - {4}", e.Id, e.EventType.Name, e.Date, e.Place?.Name, e.Description);
                }

                Console.WriteLine();
                Console.WriteLine("Births in the 1700");

                var eventsTypeDate = unitOfWork.Events.GetByEventTypeAndDate(1, new GenDate(GenDateType.Between, new DatePart(17000000), new DatePart(17990000)));
                foreach (var e in eventsTypeDate)
                {
                    Console.WriteLine("{0} - {1} {2}, {3} - {4}", e.Id, e.EventType.Name, e.Date, e.Place?.Name, e.Description);
                }

                Console.WriteLine();
                Console.WriteLine("Events in Stor-Alteren");

                var eventsPlace = unitOfWork.Events.GetByPlace(177);
                foreach (var e in eventsPlace)
                {
                    Console.WriteLine("{0} - {1} {2}, {3} - {4}", e.Id, e.EventType.Name, e.Date, e.Place?.Name, e.Description);
                }

                Console.WriteLine();
                Console.WriteLine("Events by query");

                var whereList = new List<WhereClause>
                {
                    new WhereClause(WhereJoin.And, "e.PersonId", WhereOperator.In, "@PersonId", new Dictionary<string, object> { { "PersonId", new int[] { 1, 2, 3 } } }),
                    new WhereClause(WhereJoin.And, "e.EventTypeId", WhereOperator.Equal, "@EventTypeId", new Dictionary<string, object> { { "EventTypeId", 1 } }),
                };

                var eventsQuery = unitOfWork.Events.GetByQuery(whereList);
                foreach (var e in eventsQuery)
                {
                    Console.WriteLine("{0} - {1} {2}, {3} - {4}", e.Id, e.EventType.Name, e.Date, e.Place?.Name, e.Description);
                }

                Console.WriteLine();
                Console.WriteLine("10 first Persons");

                var persons = unitOfWork.Persons.GetListPaged("", null, "FirstName, Patronym", 1, 10);
                foreach (var p in persons)
                {
                    Console.WriteLine("{0} - {1} {2} {3}, {4}, {5} ({6} - {7})", p.Id, p.FirstName, p.Patronym, p.LastName, p.Gender, p.Status, p.BornYear, p.DeathYear);
                }

                Console.WriteLine();
                Console.WriteLine("Find Persons with name");

                var personsByName = unitOfWork.Persons.FindByName("anne");
                foreach (var p in personsByName)
                {
                    Console.WriteLine("{0} - {1} {2} {3}, {4}, {5} ({6} - {7})", p.Id, p.FirstName, p.Patronym, p.LastName, p.Gender, p.Status, p.BornYear, p.DeathYear);
                }

                Console.WriteLine();
                Console.WriteLine("Find Places in Nordland");

                var placesByName = unitOfWork.Places.GetList("Name like @Name", new { Name = "%nordland%" });
                foreach (var p in placesByName)
                {
                    Console.WriteLine("{0} - {1}", p.Id, p.Name);
                }

                Console.WriteLine();
                Console.WriteLine("Find Places in Norway, paged");

                var placesByName2 = unitOfWork.Places.GetListPaged("Name like @Name", new { Name = "%norway" }, "Name", 1, 10);
                foreach (var p in placesByName2)
                {
                    Console.WriteLine("{0} - {1}", p.Id, p.Name);
                }

                Console.WriteLine();
                Console.WriteLine("Press any key...");
                Console.ReadLine();

            }
        }
    }
}
