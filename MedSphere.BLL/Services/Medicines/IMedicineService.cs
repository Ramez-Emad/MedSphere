using MedSphere.BLL.Contracts.Medicines;

namespace MedSphere.BLL.Services.Medicines
{
    public interface IMedicineService
    {
        Task<IEnumerable<MedicineResponse>> GetAllAsync(bool WithTracking = false, bool withDeleted = false, CancellationToken cancellationToken = default);

        Task<MedicineResponse?> GetByIdAsync(int id, bool withDeleted = false, CancellationToken cancellationToken = default);

        Task<int> AddAsync(MedicineRequest entity, CancellationToken cancellationToken = default);

        Task<int> Update(int id,MedicineRequest entity, CancellationToken cancellationToken = default);

        Task<int> Delete(int id, CancellationToken cancellationToken = default);

    }
}
