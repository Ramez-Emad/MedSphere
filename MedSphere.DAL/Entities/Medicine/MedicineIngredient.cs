using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.DAL.Entities.Medicine;
public class MedicineIngredient
{
    public int MedicineId { get; set; }
    public Medicine Medicine { get; set; } = default!;
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = default!;
    public string? Strength { get; set; }
}
