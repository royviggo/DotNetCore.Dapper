using DotNetCore.Data.Entities;
using System;

namespace DotNetCore.Data.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>, IDisposable
    {
        //    IEnumerable<Event> GetAllInclude();
        //    IEnumerable<Event> GetByPersonId(int personId);
    }
}
