using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.DAL.Entities.Medicine;
public class Ingredient : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<MedicineIngredient> MedicineIngredients { get; set; } = new HashSet<MedicineIngredient>();
}
