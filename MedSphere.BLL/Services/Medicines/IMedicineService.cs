using MedSphere.DAL.Entities.Medicines;

namespace MedSphere.BLL.Services.Medicines
{
    public interface IMedicineService
    {
        Task<IEnumerable<Medicine>> GetAllAsync(bool WithTracking = false, bool withDeleted = false, CancellationToken cancellationToken = default);

        Task<Medicine?> GetByIdAsync(int id, bool withDeleted = false, CancellationToken cancellationToken = default);

        Task<int> AddAsync(Medicine entity, CancellationToken cancellationToken = default);

        Task<int> Update(int id,Medicine entity, CancellationToken cancellationToken = default);

        Task<int> Delete(int id, CancellationToken cancellationToken = default);

    }
}
