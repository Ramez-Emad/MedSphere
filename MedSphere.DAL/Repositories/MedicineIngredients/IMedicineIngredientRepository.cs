using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.DAL.Repositories.MedicineIngredients;
public interface IMedicineIngredientRepository : IGenericRepository<MedicineIngredient>
{

    Task<IEnumerable<MedicineIngredient>> GetAllAsync(int mId , CancellationToken cancellationToken = default);

}
