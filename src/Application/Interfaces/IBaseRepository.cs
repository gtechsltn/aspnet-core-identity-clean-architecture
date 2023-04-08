﻿using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> filter);

        Task<T> GetByIdAsync(Guid id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
