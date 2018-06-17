using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using System.Data;
using Dapper;
using System.Linq;
using System.Collections.Generic;

namespace DotNetCore.Data.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDbFactory dbFactory, IDbTransaction dbTransaction) : base(dbFactory, dbTransaction)
        {
        }

        public override Person Get(int id)
        {
            var sql = GetBasePersonSql() + "WHERE Id = @Id";

            return Db.Context().Query<Person>(sql, param: new { Id = id }, transaction: DbTransaction).FirstOrDefault();
        }

        public override IEnumerable<Person> GetAll()
        {
            var sql = GetBasePersonSql();

            return Db.Context().Query<Person>(sql, param: new { }, transaction: DbTransaction);
        }

        private string GetBasePersonSql()
        {
            return @"
                SELECT Id,
                       FirstName,
                       FatherName,
                       Patronym,
                       LastName,
                       Gender,
                       CAST(COALESCE(BornYear, 0) AS INTEGER) AS BornYear,
                       CAST(COALESCE(DeathYear, 0) AS INTEGER) AS DeathYear,
                       Status,
                       CreatedDate,
                       ModifiedDate
                FROM Persons
                ";
        }
    }
}
