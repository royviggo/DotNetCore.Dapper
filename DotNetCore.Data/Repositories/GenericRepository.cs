using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Utils;

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

        public virtual int Add(TEntity entity)
        {
            return (int)Db.Context().Insert(entity);
        }

        public virtual int AddRange(IEnumerable<TEntity> entities)
        {
            return (int)Db.Context().Insert(entities);
        }

        public virtual bool Update(TEntity entity)
        {
            return Db.Context().Update(entity);
        }

        public virtual bool UpdateRange(IEnumerable<TEntity> entities)
        {
            return Db.Context().Update(entities);
        }

        public virtual bool Remove(int id)
        {
            return Db.Context().Delete(Get(id));
        }

        public virtual bool Remove(TEntity entity)
        {
            return Db.Context().Delete(entity);
        }

        public virtual bool RemoveRange(IEnumerable<TEntity> entities)
        {
            return Db.Context().Delete(entities);
        }

        public virtual TEntity Get(int id)
        {
            return Db.Context().Get<TEntity>(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Db.Context().GetAll<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetList(string where)
        {
            return GetList(where, null, string.Empty);
        }

        public virtual IEnumerable<TEntity> GetList(string where, object param)
        {
            return GetList(where, param, string.Empty);
        }

        public virtual IEnumerable<TEntity> GetList(string where, object param, string orderBy)
        {
            var query = GetBaseQuery().Where(where).OrderBy(orderBy);

            return GetListSql(query, param);
        }

        public virtual IEnumerable<TEntity> GetListPaged(string where, int pageNumber, int rowsPerPage)
        {
            return GetListPaged(where, null, string.Empty, pageNumber, rowsPerPage);
        }

        public virtual IEnumerable<TEntity> GetListPaged(string where, object param, int pageNumber, int rowsPerPage)
        {
            return GetListPaged(where, param, string.Empty, pageNumber, rowsPerPage);
        }

        public virtual IEnumerable<TEntity> GetListPaged(string where, object param, string orderBy, int pageNumber, int rowsPerPage)
        {
            if (pageNumber < 1 || rowsPerPage < 1)
                throw new ArgumentOutOfRangeException("Arguments pageNumber and rowsPerPage must be greater than 0");

            var query = GetBaseQuery().Where(where).OrderBy(orderBy).Paging(Db.PagingTemplate(pageNumber, rowsPerPage));

            return GetListSql(query, param);
        }

        public virtual IEnumerable<TEntity> GetListSql(string query, object param)
        {
            return Db.Context().Query<TEntity>(query, param);
        }

        public virtual string GetBaseQuery()
        {
            return typeof(TEntity).Select();
        }
    }
}