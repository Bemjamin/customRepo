using EnterpriseDal;
using System;

public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        void Reload(object entityToReload);
        AppDbContext Context { get; }
        IRepository<T> CreateRepository<T>() where T : class;
        IContextTransaction BeginTransaction();
    }