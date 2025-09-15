using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Contracts.Ingredients;
public class IngredientValidator : AbstractValidator<IngredientRequest>
{
    public IngredientValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Ingredient name is required.")
            .Length(3,50)
            .WithMessage("Ingredient name must be between 3 and 50 characters.");
    }
}
