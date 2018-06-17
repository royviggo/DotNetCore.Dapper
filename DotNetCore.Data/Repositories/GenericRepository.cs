using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using Dapper.Contrib.Extensions;
using DotNetCore.Data.Interfaces;

namespace DotNetCore.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IDisposable, new()
    {
        protected readonly IDbFactory _dbFactory;
        protected readonly IDbTransaction _transaction;

        public GenericRepository(IDbFactory dbFactory, IDbTransaction transaction)
        {
            _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        public void Dispose()
        {
            _transaction.Dispose();
            _dbFactory.Dispose();
        }

        public IDbFactory Db => _dbFactory;
        public IDbTransaction DbTransaction => _transaction;

        public virtual void Add(TEntity entity)
        {
            var result = Db.Context().Insert(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            var result = Db.Context().Insert(entities);
        }

        public virtual void Update(TEntity entity)
        {
            var result = Db.Context().Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            var result = Db.Context().Update(entities);
        }

        public virtual void Remove(int id)
        {
            var result = Db.Context().Delete(Get(id));
        }

        public virtual void Remove(TEntity entity)
        {
            var result = Db.Context().Delete(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            var result = Db.Context().Delete(entities);
        }

        public virtual TEntity Get(int id)
        {
            return Db.Context().Get<TEntity>(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Db.Context().GetAll<TEntity>();
        }
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}