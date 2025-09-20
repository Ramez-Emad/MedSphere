using Azure.Core;
using Mapster;
using MapsterMapper;
using MedSphere.BLL.Contracts.MedicineIngredients;
using MedSphere.BLL.Contracts.Medicines;
using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories.Ingredients;
using MedSphere.DAL.Repositories.MedicineIngredients;
using MedSphere.DAL.Repositories.Medicines;

namespace MedSphere.BLL.Services.Medicines
{
  
    public class MedicineService(IMedicineRepository _medicineRepository,  IMedicineIngredientRepository _medicineIngredientRepository  , IIngredientRepository _ingredientRepository) : IMedicineService
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
            if (medicine == null) 
                return null;



            var result = medicine.Adapt<MedicineResponse>();

            var ingredientDict = (await _ingredientRepository.GetAllAsync(cancellationToken: cancellationToken)).ToDictionary(i => i.Id, i => i.Name);

            result.Ingredients = medicine.MedicineIngredients
                                         .Select(mi => new MedicineIngredientResponse
                                         {
                                             Name = ingredientDict[mi.IngredientId],
                                             StrengthMg = mi.StrengthMg
                                         })
                                         .ToList();
            return result;
        }

        #endregion

        #region Add
        public async Task<MedicineResponse> AddAsync(MedicineRequest entity, CancellationToken cancellationToken = default)
        {

            #region Check of Ingredients Ids
            var existingIngredient = await _ingredientRepository.GetAllAsync(cancellationToken: cancellationToken);
            var existingIngredientIds = existingIngredient.Select(e => e.Id).ToHashSet();

            var givenIngredientIds = entity.Ingredients.Select(i => i.Id);

            if (!givenIngredientIds.All(id => (existingIngredientIds.Contains(id))))
            {
                throw new ArgumentException("One or more ingredient IDs do not exist.");
            }

            #endregion


            #region Add to Database
            var medicine = entity.Adapt<Medicine>();

            await _medicineRepository.AddAsync(medicine, cancellationToken);
            await _medicineRepository.SaveChangesAsync(cancellationToken);


            var medicineIngredient = entity.Ingredients
                           .Select(i => new MedicineIngredient
                           {
                               MedicineId = medicine.Id,
                               IngredientId = i.Id,
                               StrengthMg = i.StrengthMg
                           })
                           .ToList();

            await _medicineIngredientRepository.AddAsync(medicineIngredient, cancellationToken);
            await _medicineIngredientRepository.SaveChangesAsync(cancellationToken);
            #endregion


            #region Map to Response

            var result = entity.Adapt<MedicineResponse>();

            result.Id = medicine.Id;

            var ingredientDict = existingIngredient.ToDictionary(i => i.Id, i => i.Name);

            result.Ingredients = entity.Ingredients
                                         .Select(i => new MedicineIngredientResponse
                                         {
                                             Name = ingredientDict[i.Id],
                                             StrengthMg = i.StrengthMg
                                         })
                                         .ToList();

            #endregion

            return result ;
        }

        #endregion

        #region Update
        public async Task<int> Update(int id, MedicineRequest entity, CancellationToken cancellationToken = default)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id, false, cancellationToken);

            if (medicine == null)
                return -1;

            entity.Adapt(medicine);

            var existingIngredients = await _medicineIngredientRepository.GetAllAsync(id , cancellationToken);

            var toBeDeleted = existingIngredients.Where(ei => 
                                                            !entity.Ingredients.Any(i => i.Id == ei.IngredientId) 
                                                            ).ToList();

            var toBeAdded = entity.Ingredients.Where(i => !existingIngredients.Any(ei => ei.IngredientId == i.Id)).ToList();

            var toBeUpdated = existingIngredients.Where(ei => entity.Ingredients.Any(i => i.Id == ei.IngredientId)).ToList();

            if (toBeDeleted.Any())
            {
                foreach (var item in toBeDeleted)
                {
                    item.IsDeleted = true;
                }
            }

            if (toBeAdded.Any())
            {
                var newMedicineIngredients = toBeAdded.Select(i => new MedicineIngredient
                {
                    MedicineId = id,
                    IngredientId = i.Id,
                    StrengthMg = i.StrengthMg
                }).ToList();
                await _medicineIngredientRepository.AddAsync(newMedicineIngredients, cancellationToken);
            }

            if (toBeUpdated.Any())
            {
                foreach (var item in toBeUpdated)
                {
                    var updatedData = entity.Ingredients.First(i => i.Id == item.IngredientId);
                    item.StrengthMg = updatedData.StrengthMg;
                    item.IsDeleted = false;
                }
            }
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
