using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities.Contexts;

namespace SOC.Conductor.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        private readonly SOCDbContext _SOCDbContext;

        protected RepositoryBase(SOCDbContext _SOCDbContext)
        {
            this._SOCDbContext = _SOCDbContext;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _SOCDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            return (await _SOCDbContext.AddAsync(entity, cancellationToken)).Entity; 
        }

        public async Task<T> DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _SOCDbContext.Remove(entity);
            return await Task.FromResult(entity);
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _SOCDbContext.Update(entity);
            return await Task.FromResult(entity);
        }
    }
}
