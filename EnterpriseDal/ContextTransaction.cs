using EnterpriseDal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

public class ContextTransaction : IContextTransaction
    {
        private bool _disposed;
        private readonly IDbContextTransaction _transaction;

        public ContextTransaction(AppDbContext context, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
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