using MedSphere.DAL.Entities;

namespace MedSphere.DAL.Repositories._Generic
{
    public interface IGenericRepository <TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false, bool withDeleted = false,  CancellationToken cancellationToken = default );

        Task<TEntity?> GetByIdAsync<TKey> (TKey id, bool withDeleted = false, CancellationToken cancellationToken=default);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken=default);

        Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken=default);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);

    }
}
