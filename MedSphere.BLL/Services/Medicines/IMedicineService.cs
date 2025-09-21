using MedSphere.BLL.Abstractions;
using MedSphere.BLL.Contracts.Medicines;

namespace MedSphere.BLL.Services.Medicines
{
    public interface IMedicineService
    {
        Task<IEnumerable<MedicineResponse>> GetAllAsync(bool WithTracking = false, bool withDeleted = false, CancellationToken cancellationToken = default);

        Task<Result<MedicineResponse>> GetByIdAsync(int id, bool withDeleted = false, CancellationToken cancellationToken = default);

        Task<Result<MedicineResponse>> AddAsync(MedicineRequest entity, CancellationToken cancellationToken = default);

        Task<Result> Update(int id, MedicineRequest entity, CancellationToken cancellationToken = default);

        Task<Result> Delete(int id, CancellationToken cancellationToken = default);

    }
}
