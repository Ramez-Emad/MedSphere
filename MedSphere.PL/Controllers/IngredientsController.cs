using MedSphere.BLL.Contracts.Ingredients;
using MedSphere.BLL.Contracts.Medicines;
using MedSphere.BLL.Services.Ingredients;
using MedSphere.DAL.Entities.Medicines;
using Microsoft.AspNetCore.Http;
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
        return Ok(await _ingredientService.GetByIdAsync(id));
    }

    #endregion

    #region Create
    [HttpPost]
    public async Task<ActionResult> Create(IngredientRequest ingredient)
    {
        var validator = new IngredientValidator();
        var result = validator.Validate(ingredient);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        var created = await _ingredientService.AddAsync(ingredient);

        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);

    }

    #endregion

    #region Update

    [HttpPut("{id}")]
    public async Task<ActionResult> Edit(int id, IngredientRequest ingredient)
    {
        var validator = new IngredientValidator();
        var result = validator.Validate(ingredient);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        var res = await _ingredientService.Update(id , ingredient);

        return NoContent();
    }
    #endregion

    #region Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var res = await _ingredientService.Delete(id);
        return NoContent();

    }
    #endregion
}


  