using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DotNetCore.Data.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class, IDisposable, new()
    {
        void Dispose();

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(int id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}