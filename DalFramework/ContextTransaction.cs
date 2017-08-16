using System;
using System.Data;
using System.Data.Entity;
using DalFramework.Interface;

namespace DalFramework
{
    public class ContextTransaction<TDbContext> : IContextTransaction where TDbContext : DbContext
    {
        private bool _disposed;
        private readonly DbContextTransaction _transaction;

        public ContextTransaction(TDbContext context, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _transaction = context.Database.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                _transaction.Dispose();
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
