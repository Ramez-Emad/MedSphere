using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories.Medicines;
using System.Linq.Expressions;

namespace MedSphere.BLL.Services.Medicines
{
  
    public class MedicineService(IMedicineRepository _medicineRepository) : IMedicineService
    {
        #region GetAll
        public async Task<IEnumerable<Medicine>> GetAllAsync(bool WithTracking = false, bool withDeleted = false, CancellationToken cancellationToken = default)
            => await _medicineRepository.GetAllAsync(WithTracking, withDeleted, cancellationToken);

        #endregion

        #region GetById
        public async Task<Medicine?> GetByIdAsync(int id, bool withDeleted = false, CancellationToken cancellationToken = default)
            => await _medicineRepository.GetByIdAsync(id, withDeleted, cancellationToken);

        #endregion

        #region Add
        public async Task<int> AddAsync(Medicine entity, CancellationToken cancellationToken = default)
        {
            await _medicineRepository.AddAsync(entity, cancellationToken);
            return await _medicineRepository.SaveChangesAsync(cancellationToken);
        }
        #endregion
        
        #region Update
        public async Task<int> Update(int id, Medicine entity, CancellationToken cancellationToken = default)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id, false, cancellationToken);

            if (medicine == null)
                return 0;

          
            // Not Completed 
            medicine.Name = entity.Name;
            medicine.UpdatedOn = DateTime.UtcNow;

            return await _medicineRepository.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region Delete
        public async Task<int> Delete(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _medicineRepository.GetByIdAsync(id);
            if (entity == null)
                return 0;

            entity.IsDeleted = true;
            return await _medicineRepository.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}
