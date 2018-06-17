using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using System.Data;

namespace DotNetCore.Data.Repositories
{
    public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
    {
        public PlaceRepository(IDbFactory dbFactory, IDbTransaction transaction) : base(dbFactory, transaction)
        {
        }
    }
}
