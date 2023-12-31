﻿using System.Linq.Expressions;

namespace SOC.Conductor.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default); 
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default); 
        Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default); 
        Task<List<T>> GetAllAsync(CancellationToken  cancellationToken = default); 
        Task<IList<T>> GetByCondition(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default); 
    }
}
