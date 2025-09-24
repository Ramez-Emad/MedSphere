using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories._Generic;

namespace MedSphere.DAL.Repositories.Ingredients;
public interface IIngredientRepository : IGenericRepository<Ingredient>
{
    Task<bool> IsIngredientNameExists(string name, CancellationToken cancellationToken = default);
    Task<bool> IsIngredientNameExists(int id, string name, CancellationToken cancellationToken = default);
}
