using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities.Contexts;

namespace SOC.Conductor.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        public SOCDbContext SOCDbContext;
        public RepositoryBase(SOCDbContext SOCDbContext)
        {
            this.SOCDbContext = SOCDbContext;
        }
        //public async Task<ICollection< T> GetAllAsync()
        //{
        //    return await SOCDbContext.Set<T>();
        //}
        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
