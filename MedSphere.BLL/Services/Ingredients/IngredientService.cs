using Mapster;
using MedSphere.BLL.Contracts.Ingredients;
using MedSphere.BLL.Contracts.Medicines;
using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Services.Ingredients;
public class IngredientService(IIngredientRepository _ingredientRepository) : IIngredientService
{



    #region GetAll
    public async Task<IEnumerable<IngredientResponse>> GetAllAsync(bool WithTracking = false, bool withDeleted = false, CancellationToken cancellationToken = default)
    {
        var medicines = await _ingredientRepository.GetAllAsync(WithTracking, withDeleted, cancellationToken);
        return medicines.Adapt<IEnumerable<IngredientResponse>>();

    }
    #endregion

    #region GetById
    public async Task<IngredientResponse?> GetByIdAsync(int id, bool withDeleted = false, CancellationToken cancellationToken = default)
    {
        var medicine = await _ingredientRepository.GetByIdAsync(id, withDeleted, cancellationToken);
        if (medicine == null)
            return null;
        return medicine.Adapt<IngredientResponse>();
    }

    #endregion

    #region Add
    public async Task<IngredientResponse> AddAsync(IngredientRequest entity, CancellationToken cancellationToken = default)
    {
        var ingredient = entity.Adapt<Ingredient>();

        await _ingredientRepository.AddAsync(ingredient, cancellationToken);
        await _ingredientRepository.SaveChangesAsync(cancellationToken);

        return ingredient.Adapt<IngredientResponse>();
    }
    #endregion

    #region Update
    public async Task<int> Update(int id, IngredientRequest entity, CancellationToken cancellationToken = default)
    {
        var ingredient = await _ingredientRepository.GetByIdAsync(id, false, cancellationToken);

        if (ingredient == null)
            return -1;

        entity.Adapt(ingredient);

        ingredient.UpdatedOn = DateTime.UtcNow;

        return await _ingredientRepository.SaveChangesAsync(cancellationToken);
    }
    #endregion

    #region Delete
    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _ingredientRepository.GetByIdAsync(id, false, cancellationToken);
        if (entity == null)
            return false;

        entity.IsDeleted = true;
        return await _ingredientRepository.SaveChangesAsync(cancellationToken) > 0;
    }

    #endregion









}
