using System;
using System.IO;
using DotNetCore.Data.Database;

namespace DotNetCore
{
    public class Program
    {
        public static void Main()
        {
            var connectionString = "Data Source=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "DotNetCore.sqlite");
            var unitOfWork = new UnitOfWork(new SqliteDbFactory(connectionString));

            var events = unitOfWork.Events.GetByPersonId(1);
            foreach (var e in events)
            {
                Console.WriteLine("{0} - {1} | {2} {3}, {4} - {5}", e.Id, e.EventTypeId, e.EventType.Name, e.Date, e.Place?.Name, e.Description);
            }

            var persons = unitOfWork.Persons.GetAll();
            foreach (var p in persons)
            {
                Console.WriteLine("{0} - {1} {2} {3}, {4}, {5} ({6} - {7})", p.Id, p.FirstName, p.Patronym, p.LastName, p.Gender, p.Status, p.BornYear, p.DeathYear);
            }
        }
    }
}
