using System.Linq.Expressions;

namespace MedSphere.DAL.Repositories._Generic
{
    public interface IGenericRepository <TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false, Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default );

        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken=default);
        Task<TEntity?> GetByIdAsync<TKey> (TKey id, CancellationToken cancellationToken=default);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken=default);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);

    }
}
