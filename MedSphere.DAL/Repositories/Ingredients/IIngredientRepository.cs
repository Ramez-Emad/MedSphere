using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.DAL.Repositories.Ingredients;
public interface IIngredientRepository : IGenericRepository<Ingredient>
{
    Task<bool> IsIngredientNameExists(string name, CancellationToken cancellationToken = default);
}
