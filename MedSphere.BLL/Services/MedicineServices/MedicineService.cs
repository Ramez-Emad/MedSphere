using MedSphere.DAL.Entities.MedicineEntities;
using MedSphere.DAL.Repositories.MedicineRepo;
using System.Linq.Expressions;

namespace MedSphere.BLL.Services.MedicineServices
{
  
    public class MedicineService(IMedicineRepository _medicineRepository) : IMedicineService
    {
        public async Task<IEnumerable<Medicine>> GetAllMedicinesAsync(bool WithTracking = false, Expression<Func<Medicine, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await _medicineRepository.GetAllAsync(WithTracking, filter, cancellationToken);
        }

        public async Task<Medicine?> GetMedicineAsync(Expression<Func<Medicine, bool>> filter, CancellationToken cancellationToken = default)
        {
            
            return await _medicineRepository.GetAsync(filter, cancellationToken);
        }
        public async Task<int> AddMedicineAsync(Medicine entity, CancellationToken cancellationToken = default)
        {
            await _medicineRepository.AddAsync(entity, cancellationToken);
            return await _medicineRepository.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> UpdateMedicine(Medicine entity, CancellationToken cancellationToken = default)
        {
            _medicineRepository.Update(entity);
            return await _medicineRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteMedicine(Medicine entity, CancellationToken cancellationToken = default)
        {
            entity.IsDeleted = true;
            _medicineRepository.Update(entity);
            return await _medicineRepository.SaveChangesAsync(cancellationToken);
        }     

    }
}
