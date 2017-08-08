using EnterpriseDal;
using System;
using System.Collections.Generic;
using System.Linq;

public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        //private IEnumerable<IRepository<T>> repositories;
        private List<Object> _repositoryList = new List<object>();
        private readonly AppDbContext _context;

        public void Reload(object entityToReload)
        {
            _context.Entry(entityToReload).Reload();
        }

        public AppDbContext Context
        {
            get
            {
                return _context;
            }
        }

    public IRepository<T> CreateRepository<T>() where T : class
        {
            var existingRepository = _repositoryList.FirstOrDefault(x => x.GetType() == typeof(Repository<T>));
            if (existingRepository != null)
            {
                return (Repository<T>)existingRepository;
            }
            var newRepo = new Repository<T>(this);
            _repositoryList.Add(newRepo);
            return newRepo;
        }

        public UnitOfWork()
        {
            _context = new AppDbContext();
        }

        public IContextTransaction BeginTransaction()
        {
            return new ContextTransaction(_context);
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