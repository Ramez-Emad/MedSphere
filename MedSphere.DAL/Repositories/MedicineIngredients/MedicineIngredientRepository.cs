using MedSphere.DAL.Data;
using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.DAL.Repositories.MedicineIngredients;
public class MedicineIngredientRepository(AppDbContext appDbContext) : GenericRepository<MedicineIngredient>(appDbContext), IMedicineIngredientRepository
{
}
