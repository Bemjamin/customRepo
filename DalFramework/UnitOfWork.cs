using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DalFramework.Interface;

namespace DalFramework
{
    public abstract class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext, new()
    {
        private bool _disposed;
        private List<Object> _repositoryList = new List<object>();
        private readonly TDbContext _context;

        public void Reload(object entityToReload)
        {
            _context.Entry(entityToReload).Reload();
        }

        public TDbContext Context => _context;

        public IRepository<T> CreateRepository<T>() where T : class
        {
            var existingRepository = _repositoryList.FirstOrDefault(x => x.GetType() == typeof(Repository<T, TDbContext>));
            if (existingRepository != null)
            {
                return (Repository<T, TDbContext>)existingRepository;
            }
            var newRepo = new Repository<T, TDbContext>(this);
            _repositoryList.Add(newRepo);
            return newRepo;
        }

        public UnitOfWork()
        {
            _context = new TDbContext();
        }

        public IContextTransaction BeginTransaction()
        {
            return new ContextTransaction<TDbContext>(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _repositoryList.ForEach(obj =>
                    {
                        ((IDisposable)obj).Dispose();
                    });
                    _repositoryList = null;
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
