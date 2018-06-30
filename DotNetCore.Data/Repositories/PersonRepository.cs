using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Utils;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace DotNetCore.Data.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDbFactory dbFactory, IDbTransaction dbTransaction) : base(dbFactory, dbTransaction)
        {
        }

        public override Person Get(int id)
        {
            var sql = GetBaseQuery().Where("Id = @Id");

            return Db.Context().QuerySingle<Person>(sql, param: new { Id = id }, transaction: DbTransaction);
        }

        public override IEnumerable<Person> GetAll()
        {
            var sql = GetBaseQuery();

            return Db.Context().Query<Person>(sql, param: new { }, transaction: DbTransaction);
        }

        public IEnumerable<Person> FindByName(string name)
        {
            var sql = GetBaseQuery().Where("FirstName like @Name OR Patronym like @Name OR LastName like @Name");

            return Db.Context().Query<Person>(sql, param: new { Name = name + "%" }, transaction: DbTransaction);
        }

        public override string GetBaseQuery()
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
                FROM   Persons
                ";
        }
    }
}
