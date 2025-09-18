using MedSphere.DAL.Data;
using MedSphere.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedSphere.DAL.Repositories._Generic
{
    public class GenericRepository<TEntity>(AppDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        #region GetAll
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false, bool withDeleted = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (!withDeleted)
                query = query.Where(E => !E.IsDeleted);

            if (!withTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync(cancellationToken);
        }

        #endregion

        #region GetById
        public async Task<TEntity?> GetByIdAsync<TKey>(TKey id, bool withDeleted = false, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(new object?[] { id }, cancellationToken);
            if (entity is null || (!withDeleted && entity.IsDeleted))
                return null;

            return entity;
        }
        #endregion
        
        #region Add 
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        public async Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

        #endregion

        #region Update
        public void Update(TEntity entity)
            => _dbContext.Update(entity); 

        #endregion
       
        #region Delete
        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        #endregion
       
        #region Save Changes
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
           => await _dbContext.SaveChangesAsync(cancellationToken);

        #endregion

    }
}
