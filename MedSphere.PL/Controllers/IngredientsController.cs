using MedSphere.BLL.Contracts.Ingredients;
using MedSphere.BLL.Contracts.Medicines;
using MedSphere.BLL.Services.Ingredients;
using MedSphere.DAL.Entities.Medicines;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MedSphere.PL.Controllers;
[Route("api/[controller]")]
[ApiController]
public class IngredientsController(IIngredientService _ingredientService) : ControllerBase
{
    #region GetAll

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await _ingredientService.GetAllAsync());
    }
    #endregion

    #region GetById

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var result = await _ingredientService.GetByIdAsync(id);

        return result.IsSuccess
                     ? Ok(result.Value)
                     : BadRequest(result.Error);
    }

    #endregion

    #region Create
    [HttpPost]
    public async Task<ActionResult> Create(IngredientRequest ingredient)
    {
        var result = await _ingredientService.AddAsync(ingredient);

        return result.IsSuccess
                     ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
                     : BadRequest(result.Error);
    }

    #endregion

    #region Update

    [HttpPut("{id}")]
    public async Task<ActionResult> Edit(int id, IngredientRequest ingredient)
    {
        var result = await _ingredientService.Update(id , ingredient);

        return result.IsSuccess
                     ? NoContent()
                     : BadRequest(result.Error);
    }
    #endregion

    #region Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _ingredientService.Delete(id);

        return result.IsSuccess
                    ? NoContent()
                    : BadRequest(result.Error);

    }
    #endregion
}


  