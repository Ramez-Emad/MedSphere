using MapsterMapper;
using MedSphere.BLL.Contracts.Medicines;
using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories.Medicines;

namespace MedSphere.BLL.Services.Medicines
{
  
    public class MedicineService(IMedicineRepository _medicineRepository, IMapper _mapper) : IMedicineService
    {
        #region GetAll
        public async Task<IEnumerable<MedicineResponse>> GetAllAsync(bool WithTracking = false, bool withDeleted = false, CancellationToken cancellationToken = default)
        {
            var medicines = await _medicineRepository.GetAllAsync(WithTracking, withDeleted, cancellationToken);
            //return medicines.Adapt<IEnumerable<MedicineResponse>>();
            return _mapper.Map<IEnumerable<MedicineResponse>>(medicines); 
        }
        #endregion

        #region GetById
        public async Task<MedicineResponse?> GetByIdAsync(int id, bool withDeleted = false, CancellationToken cancellationToken = default)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id, withDeleted, cancellationToken);
            if (medicine == null) 
                return null;
            //return medicine.Adapt<MedicineResponse>();
            return _mapper.Map<MedicineResponse>(medicine);
        }

        #endregion

        #region Add
        public async Task<MedicineResponse> AddAsync(MedicineRequest entity, CancellationToken cancellationToken = default)
        {
            //var medicine = entity.Adapt<Medicine>();
            var medicine = _mapper.Map<Medicine>(entity);
            
            await _medicineRepository.AddAsync(medicine, cancellationToken);
            await _medicineRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<MedicineResponse>(medicine);
        }
        #endregion
        
        #region Update
        public async Task<int> Update(int id, MedicineRequest entity, CancellationToken cancellationToken = default)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id, false, cancellationToken);

            if (medicine == null)
                return -1;

            //entity.Adapt(medicine);
            _mapper.Map(entity, medicine); 
            medicine.UpdatedOn = DateTime.UtcNow;

            return await _medicineRepository.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _medicineRepository.GetByIdAsync(id, false, cancellationToken);
            if (entity == null)
                return false;

            entity.IsDeleted = true;
            return await _medicineRepository.SaveChangesAsync(cancellationToken) > 0 ;
        }

        #endregion
    }
}
