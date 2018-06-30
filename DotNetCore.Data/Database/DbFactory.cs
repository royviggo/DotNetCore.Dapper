using DotNetCore.Data.Interfaces;
using System;
using System.Data;

namespace DotNetCore.Data.Database
{
    public abstract class DbFactory : IDbFactory
    {
        private bool _disposed = false;
        private string _connectionString;
        private IDbConnection _dbContext;

        protected DbFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _dbContext = Connection(_connectionString);
        }

        public abstract IDbConnection Connection(string connectionString);

        public virtual string IdentitySql()
        {
            return "";
        }

        public virtual string PagingTemplate(int pageNumber, int rowsPerPage)
        {
            return "{0}";
        }

        public virtual string EncapsulationTemplate()
        {
            return "{0}";
        }

        public virtual IDbConnection Context()
        {
            return _dbContext ?? (_dbContext = Connection(_connectionString));
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _dbContext?.Dispose();
                _dbContext = null;
                _disposed = true;
            }
        }
    }
}
