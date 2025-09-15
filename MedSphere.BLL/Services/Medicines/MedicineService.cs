using Mapster;
using MedSphere.BLL.Contracts.Medicines;
using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories.Medicines;

namespace MedSphere.BLL.Services.Medicines
{
  
    public class MedicineService(IMedicineRepository _medicineRepository) : IMedicineService
    {
        #region GetAll
        public async Task<IEnumerable<MedicineResponse>> GetAllAsync(bool WithTracking = false, bool withDeleted = false, CancellationToken cancellationToken = default)
        {
            var medicines = await _medicineRepository.GetAllAsync(WithTracking, withDeleted, cancellationToken);
            return medicines.Adapt<IEnumerable<MedicineResponse>>();
        }
        #endregion

        #region GetById
        public async Task<MedicineResponse?> GetByIdAsync(int id, bool withDeleted = false, CancellationToken cancellationToken = default)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id, withDeleted, cancellationToken);
            return medicine.Adapt<MedicineResponse>();
        }

        #endregion

        #region Add
        public async Task<int> AddAsync(MedicineRequest entity, CancellationToken cancellationToken = default)
        {
            var medicine = entity.Adapt<Medicine>(); 
            await _medicineRepository.AddAsync(medicine, cancellationToken);
            return await _medicineRepository.SaveChangesAsync(cancellationToken);
        }
        #endregion
        
        #region Update
        public async Task<int> Update(int id, MedicineRequest entity, CancellationToken cancellationToken = default)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id, false, cancellationToken);

            if (medicine == null)
                return 0;

            entity.Adapt(medicine);

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
