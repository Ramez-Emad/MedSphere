using MedSphere.DAL.Entities.Medicines;
using System.Linq.Expressions;

namespace MedSphere.BLL.Services.Medicines
{
    public interface IMedicineService
    {
        Task<IEnumerable<Medicine>> GetAllMedicinesAsync(bool WithTracking = false, Expression<Func<Medicine, bool>> filter = null!, CancellationToken cancellationToken = default);

        Task<Medicine?> GetMedicineAsync(Expression<Func<Medicine, bool>> filter, CancellationToken cancellationToken = default);
        Task<Medicine?> GetMedicineByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<int> AddMedicineAsync(Medicine entity, CancellationToken cancellationToken = default);

        Task<int> UpdateMedicine(Medicine entity, CancellationToken cancellationToken = default);

        Task<int> DeleteMedicine(Medicine entity, CancellationToken cancellationToken = default);

    }
}
