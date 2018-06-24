using DotNetCore.Data.Entities;
using System.Collections.Generic;

namespace DotNetCore.Data.Interfaces
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        IEnumerable<Person> FindByName(string name);
    }
}
