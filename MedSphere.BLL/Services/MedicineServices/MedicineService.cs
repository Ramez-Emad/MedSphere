using MedSphere.DAL.Entities.MedicineEntities;
using MedSphere.DAL.Repositories.MedicineRepo;
using System.Linq.Expressions;

namespace MedSphere.BLL.Services.MedicineServices
{
    public class MedicineService(IMedicineRepository _medicineRepository) : IMedicineService
    {
        public async Task<IEnumerable<Medicine>> GetAllMedicinesAsync(bool WithTracking = false, Expression<Func<Medicine, bool>> filter = null)
        {
            return await _medicineRepository.GetAllMedicinesAsync(WithTracking, filter);
        }

        public async Task<Medicine?> GetMedicineAsync(Expression<Func<Medicine, bool>> filter)
        {
            return await _medicineRepository.GetMedicineAsync(filter);
        }
        public async Task<int> AddMedicineAsync(Medicine entity)
        {
            await _medicineRepository.AddMedicineAsync(entity);
            return await _medicineRepository.SaveChangesAsync();
        }
        public async Task<int> UpdateMedicine(Medicine entity)
        {
            _medicineRepository.UpdateMedicine(entity);
            return await _medicineRepository.SaveChangesAsync();
        }

        public async Task<int> DeleteMedicine(Medicine entity)
        {
            _medicineRepository.DeleteMedicine(entity);
            return await _medicineRepository.SaveChangesAsync();
        }     

    }
}
