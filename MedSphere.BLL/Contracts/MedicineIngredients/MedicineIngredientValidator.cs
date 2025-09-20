using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Contracts.MedicineIngredients;
public class MedicineIngredientValidator : AbstractValidator<MedicineIngredientRequest>
{
    public MedicineIngredientValidator()
    {
        RuleFor(x => x.StrengthMg)
            .GreaterThan(0)
            .WithMessage("Strength in mg must be greater than 0");
    }
}
