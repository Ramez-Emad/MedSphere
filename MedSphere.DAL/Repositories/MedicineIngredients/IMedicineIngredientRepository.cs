using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories._Generic;

namespace MedSphere.DAL.Repositories.MedicineIngredients;
public interface IMedicineIngredientRepository : IGenericRepository<MedicineIngredient>
{

    Task<IEnumerable<MedicineIngredient>> GetAllAsync(int mId , CancellationToken cancellationToken = default);

}
