using Azure.Core;
using Mapster;
using MedSphere.BLL.Abstractions;
using MedSphere.BLL.Contracts.Ingredients;
using MedSphere.BLL.Errors.Ingredients;
using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories.Ingredients;
using Microsoft.Extensions.Logging;

namespace MedSphere.BLL.Services.Ingredients;
public class IngredientService(IIngredientRepository _ingredientRepository , ILogger<IngredientService> _logger) : IIngredientService
{



    #region GetAll
    public async Task<IEnumerable<IngredientResponse>> GetAllAsync(bool WithTracking = false, bool withDeleted = false, CancellationToken cancellationToken = default)
    {
        var medicines = await _ingredientRepository.GetAllAsync(WithTracking, withDeleted, cancellationToken);
        return medicines.Adapt<IEnumerable<IngredientResponse>>();

    }
    #endregion

    #region GetById
    public async Task<Result<IngredientResponse?>> GetByIdAsync(int id, bool withDeleted = false, CancellationToken cancellationToken = default)
    {
        var ingredient = await _ingredientRepository.GetByIdAsync(id, withDeleted, cancellationToken);

        if (ingredient == null)
            return Result.Failure<IngredientResponse?>(IngredientsErrors.IngredientNotFound);

        return Result.Success<IngredientResponse?>(ingredient.Adapt<IngredientResponse>());
    }

    #endregion

    #region Add
    public async Task<Result<IngredientResponse>> AddAsync(IngredientRequest entity, CancellationToken cancellationToken = default)
    {

        if (await _ingredientRepository.IsIngredientNameExists(entity.Name, cancellationToken) is not null)
            return Result.Failure<IngredientResponse>(IngredientsErrors.IngredientNameAlreadyExists);

        var ingredient = entity.Adapt<Ingredient>();

        await _ingredientRepository.AddAsync(ingredient, cancellationToken);
        await _ingredientRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(ingredient.Adapt<IngredientResponse>());
    }
    #endregion

    #region Update
    public async Task<Result> Update(int id, IngredientRequest entity, CancellationToken cancellationToken = default)
    {
        var ingredient = await _ingredientRepository.GetByIdAsync(id, false, cancellationToken);

        if (ingredient == null)
            return Result.Failure(IngredientsErrors.IngredientNotFound);


        if ( (await _ingredientRepository.IsIngredientNameExists(entity.Name, cancellationToken) is { } val && val != id))
            return Result.Failure<IngredientResponse>(IngredientsErrors.IngredientNameAlreadyExists);

        entity.Adapt(ingredient);

        await _ingredientRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
    #endregion

    #region Delete
    public async Task<Result> Delete(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _ingredientRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Result.Failure(IngredientsErrors.IngredientNotFound);

        entity.IsDeleted = true;
        await _ingredientRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    #endregion









}
