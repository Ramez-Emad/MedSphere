using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Contracts.MedicineIngredients;
public class MedicineIngredientRequest
{
    public int Id { get; set; }
    public int StrengthMg { get; set; }
}
