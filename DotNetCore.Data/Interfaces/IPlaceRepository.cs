using DotNetCore.Data.Entities;
using System;

namespace DotNetCore.Data.Interfaces
{
    public interface IPlaceRepository : IGenericRepository<Place>, IDisposable
    {
    }
}
