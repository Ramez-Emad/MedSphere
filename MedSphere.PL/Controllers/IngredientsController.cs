using MedSphere.BLL.Consts;
using MedSphere.BLL.Contracts.Ingredients;
using MedSphere.BLL.Services.Auth.Jwt;
using MedSphere.BLL.Services.Ingredients;
using MedSphere.DAL.Entities.Auth;
using MedSphere.PL.Extensions;
using MedSphere.PL.Filters.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedSphere.PL.Controllers;
[Route("api/[controller]")]
[ApiController]
public class IngredientsController(IIngredientService _ingredientService) : ControllerBase
{
    #region GetAll

    [HttpGet]
    [HasPermission(Permissions.GetIngredients)]
    public async Task<ActionResult> GetAll()
    {
       return Ok(await _ingredientService.GetAllAsync());
    }
    #endregion

    #region GetById

    [HttpGet("{id}")]
    [HasPermission(Permissions.GetIngredients)]
    public async Task<ActionResult> Get(int id)
    {
        var result = await _ingredientService.GetByIdAsync(id);

        return result.IsSuccess
                     ? Ok(result.Value)
                     : result.ToProblem();
    }

    #endregion

    #region Create
    [HttpPost]
    [HasPermission(Permissions.AddIngredients)]
    public async Task<ActionResult> Create(IngredientRequest ingredient)
    {
        var result = await _ingredientService.AddAsync(ingredient);

        return result.IsSuccess
                     ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
                     : result.ToProblem();
    }

    #endregion

    #region Update

    [HttpPut("{id}")]
    [HasPermission(Permissions.UpdateIngredients)]
    public async Task<ActionResult> Edit(int id, IngredientRequest ingredient)
    {
        var result = await _ingredientService.Update(id , ingredient);

        return result.IsSuccess
                     ? NoContent()
                     : result.ToProblem();
    }
    #endregion

    #region Delete
    [HttpDelete("{id}")]
    [HasPermission(Permissions.DeleteIngredients)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _ingredientService.Delete(id);

        return result.IsSuccess
                    ? NoContent()
                    : result.ToProblem();

    }
    #endregion
}


  