using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Contracts.MedicineIngredients;
public class MedicineIngredientResponse
{
    public string Name { get; set; } = default!;
    public int? StrengthMg { get; set; }
}
