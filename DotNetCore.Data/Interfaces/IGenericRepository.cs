using System;
using System.Collections.Generic;

namespace DotNetCore.Data.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class, IDisposable, new()
    {
        void Dispose();

        int Add(TEntity entity);
        int AddRange(IEnumerable<TEntity> entities);
        bool Update(TEntity entity);
        bool UpdateRange(IEnumerable<TEntity> entities);
        bool Remove(int id);
        bool Remove(TEntity entity);
        bool RemoveRange(IEnumerable<TEntity> entities);

        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetList(string where);
        IEnumerable<TEntity> GetList(string where, object param);
        IEnumerable<TEntity> GetListPaged(string where, int pageNumber, int rowsPerPage);
        IEnumerable<TEntity> GetListPaged(string where, object param, int pageNumber, int rowsPerPage);
        IEnumerable<TEntity> GetListPaged(string where, object param, string orderBy, int pageNumber, int rowsPerPage);
    }
}