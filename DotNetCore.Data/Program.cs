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

            Console.WriteLine("Places");

            var places = unitOfWork.Places.GetAll();
            foreach (var p in places)
            {
                Console.WriteLine("{0} - {1}", p.Id, p.Name);
            }

            Console.WriteLine();
            Console.WriteLine("Events for a person");

            var events = unitOfWork.Events.GetByPersonId(1);
            foreach (var e in events)
            {
                Console.WriteLine("{0} - {1} {2}, {3} - {4}", e.Id, e.EventType.Name, e.Date, e.Place?.Name, e.Description);
            }

            Console.WriteLine();
            Console.WriteLine("Persons");

            var persons = unitOfWork.Persons.GetAll();
            foreach (var p in persons)
            {
                Console.WriteLine("{0} - {1} {2} {3}, {4}, {5} ({6} - {7})", p.Id, p.FirstName, p.Patronym, p.LastName, p.Gender, p.Status, p.BornYear, p.DeathYear);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
