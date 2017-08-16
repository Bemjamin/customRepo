using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DalFramework.Interface;

namespace DalFramework
{
    public class Repository<T, TDbContext> : IDisposable, IRepository<T> where T : class where TDbContext : DbContext
    {
        private bool _disposed;

        private readonly IUnitOfWork<TDbContext> _unitOfWork;
        
        public Repository(IUnitOfWork<TDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
     
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _unitOfWork.Context.Set<T>().Where(predicate);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _unitOfWork.Context.Set<T>();
            query = includes.Aggregate(query, (current, next) => current.Include(next));
            return query.Where(predicate);            
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            IQueryable<T> query = _unitOfWork.Context.Set<T>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query.Where(predicate);    
        }

        public void SetValues(T entity, T newEntity)
        {
            _unitOfWork.Context.Entry(entity).CurrentValues.SetValues(newEntity);
        }

        public void Add(T entity)
        {
            _unitOfWork.Context.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            _unitOfWork.Context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            var dbSet = _unitOfWork.Context.Set<T>();
            if (dbSet != null)
            {
                entities.ToList().ForEach(x => dbSet.Remove(x));    
            }
        }

        public void AddRange(IEnumerable<T> entities)
        {
            var dbSet = _unitOfWork.Context.Set<T>();
            if (dbSet != null)
            {
                entities.ToList().ForEach(x => dbSet.Add(x));    
            }
            
        }

        public void Update(T entity)
        {
            _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
        }

        public void Attach(T entity)
        {
            _unitOfWork.Context.Set<T>().Attach(entity);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
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
