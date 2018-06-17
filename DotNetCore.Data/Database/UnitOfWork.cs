using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Repositories;
using System;
using System.Data;

namespace DotNetCore.Data.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;

        public UnitOfWork(IDbFactory dbFactory)
        {
            Db = dbFactory;
            Db.Context().Open();
            DbTransaction = Db.Context().BeginTransaction();
        }

        public IDbFactory Db { get; private set; }
        public IDbTransaction DbTransaction { get; private set; }

        private IEventRepository _eventRepository;
        public IEventRepository Events => _eventRepository ?? (_eventRepository = new EventRepository(Db, DbTransaction));

        private IEventTypeRepository _eventTypeRepository;
        public IEventTypeRepository EventTypes => _eventTypeRepository ?? (_eventTypeRepository = new EventTypeRepository(Db, DbTransaction));

        private IPersonRepository _personRepository;
        public IPersonRepository Persons => _personRepository ?? (_personRepository = new PersonRepository(Db, DbTransaction));

        private IPlaceRepository _placeRepository;
        public IPlaceRepository Places => _placeRepository ?? (_placeRepository = new PlaceRepository(Db, DbTransaction));


        private void ResetRepositories()
        {
            _placeRepository = null;
        }

        public void Save()
        {
            try
            {
                DbTransaction.Commit();
            }
            catch
            {
                DbTransaction.Rollback();
                throw;
            }
            finally
            {
                DbTransaction.Dispose();
                DbTransaction = Db.Context().BeginTransaction();
                ResetRepositories();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                if (DbTransaction != null)
                {
                    DbTransaction.Dispose();
                    DbTransaction = null;
                }

                if (Db != null)
                {
                    Db.Dispose();
                    Db = null;
                }

                disposed = true;
            }
        }
    }
}