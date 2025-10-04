using MedSphere.BLL.Abstractions;
using Microsoft.AspNetCore.Http;

namespace MedSphere.BLL.Errors.Ingredients;
public class IngredientErrors
{
    public static readonly Error IngredientNotFound =
        new("Ingredient.NotFound", "No Ingredient was found with the given ID", StatusCodes.Status404NotFound);

    public static readonly Error IngredientNameAlreadyExists =
        new("Ingredient.NameAlreadyExists", "An Ingredient with the same name already exists", StatusCodes.Status409Conflict);

}
