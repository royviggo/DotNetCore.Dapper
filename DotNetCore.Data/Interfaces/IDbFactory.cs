using System;
using System.Data;

namespace DotNetCore.Data.Interfaces
{
    public interface IDbFactory : IDisposable
    {
        IDbConnection Context();
    }
}   