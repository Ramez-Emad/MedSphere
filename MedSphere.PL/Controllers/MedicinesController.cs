using MedSphere.BLL.Contracts.Medicines;
using MedSphere.BLL.Services.Medicines;
using Microsoft.AspNetCore.Mvc;

namespace MedSphere.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController(IMedicineService _service) : ControllerBase
    {
        #region GetAll

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var medicines = await _service.GetAllAsync();
            return Ok(medicines);
        }
        #endregion

        #region GetById

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.IsSuccess
                         ? Ok(result.Value)
                         : BadRequest(result.Error);
        }

        #endregion

        #region Create
        [HttpPost]
        public async Task<ActionResult> Create(MedicineRequest medicine)
        {
            //var validator = new MedicineValidator();
            //var ValidationResult = validator.Validate(medicine);

            //if (!ValidationResult.IsValid)
            //{
            //    return BadRequest(ValidationResult.Errors);
            //}

            var result = await _service.AddAsync(medicine);

            return result.IsSuccess
                         ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
                         : BadRequest(result.Error);

        }

        #endregion

        #region Update

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, MedicineRequest medicine)
        {
            var validator = new MedicineValidator();
            var ValidationResult = validator.Validate(medicine);

            if (!ValidationResult.IsValid)
            {
                return BadRequest(ValidationResult.Errors);
            }

            var result = await _service.Update(id, medicine);

            return result.IsSuccess
                         ? NoContent()
                         : BadRequest(result.Error);
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            return result.IsSuccess
                         ? NoContent()
                         : BadRequest(result.Error);

        }
        #endregion

    }
}
