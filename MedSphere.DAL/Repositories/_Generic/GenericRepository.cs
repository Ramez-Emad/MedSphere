using MedSphere.DAL.Data;
using MedSphere.DAL.Entities.Medicines;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MedSphere.DAL.Repositories._Generic
{
    public class GenericRepository<TEntity>(AppDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : class
    {
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken=default)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false, Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            
            if (filter is not null)
                query = query.Where(filter);

            if (!WithTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken=default)
        {
            return await _dbContext.Set<TEntity>()
                                   .Where(filter)
                                   .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id, cancellationToken);

        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken=default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken); 
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }
    }
}
