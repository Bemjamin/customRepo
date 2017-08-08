using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public interface IRepository<T> where T : class
    {
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] includes);
        void SetValues(T entity, T newEntity);
        void Add(T entity);
        void Remove(T entity);
        void AddRange(IEnumerable<T> entity);
        void RemoveRange(IEnumerable<T> entity);
        void Update(T entity);
        void Attach(T entity);
    }