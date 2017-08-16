using System;
using System.Data.Entity;

namespace DalFramework.Interface
{
    public interface IUnitOfWork<out TDbContext>  : IDisposable where TDbContext : DbContext
    {
        void SaveChanges();
        void Reload(object entityToReload);
        TDbContext Context { get; }
        IRepository<T> CreateRepository<T>() where T : class;
        IContextTransaction BeginTransaction();
    }
}
