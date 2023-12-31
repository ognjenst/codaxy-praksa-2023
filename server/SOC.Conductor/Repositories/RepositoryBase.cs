﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities.Contexts;
using System.Linq.Expressions;
using System.Reflection;

namespace SOC.Conductor.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        protected readonly SOCDbContext _dbContext;

        protected RepositoryBase(SOCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            return (await _dbContext.AddAsync(entity, cancellationToken)).Entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Update(entity);
            return await Task.FromResult(entity);
        }

        public async Task<T> DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Remove(entity);
            return await Task.FromResult(entity);
        }

        public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<IList<T>> GetByCondition(Expression<Func<T, bool>> condition, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().Where(condition).ToListAsync();
        }
    }
}
