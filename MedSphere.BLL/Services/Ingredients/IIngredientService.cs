using MedSphere.BLL.Abstractions;
using MedSphere.BLL.Contracts.Ingredients;

namespace MedSphere.BLL.Services.Ingredients;
public interface IIngredientService
{
    Task<IEnumerable<IngredientResponse>> GetAllAsync(bool WithTracking = false, bool withDeleted = false, CancellationToken cancellationToken = default);

    Task<Result<IngredientResponse?>> GetByIdAsync(int id, bool withDeleted = false, CancellationToken cancellationToken = default);

    Task<Result<IngredientResponse>> AddAsync(IngredientRequest entity, CancellationToken cancellationToken = default);

    Task<Result> Update(int id, IngredientRequest entity, CancellationToken cancellationToken = default);

    Task<Result> Delete(int id, CancellationToken cancellationToken = default);



}
