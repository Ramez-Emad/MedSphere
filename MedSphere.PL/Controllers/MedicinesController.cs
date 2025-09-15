using MedSphere.BLL.Contracts.Medicines;
using MedSphere.BLL.Services.Medicines;
using Microsoft.AspNetCore.Mvc;

namespace MedSphere.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController(IMedicineService _service) : ControllerBase
    {
        #region Get

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var medicines = await _service.GetAllAsync();
            return Ok(medicines);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var medicine = await _service.GetByIdAsync(id);
            if (medicine == null)
                return NotFound(); 

            return Ok(medicine);
        }

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> Create(MedicineRequest medicine)
        {
            var validator = new MedicineValidator();
            var result = validator.Validate(medicine);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            
            var created = await _service.AddAsync(medicine);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            
        }

        #endregion
     
        #region Put

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, MedicineRequest medicine)
        {
            var validator = new MedicineValidator();
            var result = validator.Validate(medicine);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var res = await _service.Update(id, medicine);

            if (res == -1 ) // No Medicines with this id 
                return NotFound();

            //else if ( res == 0 ) // No Changes Found or Error Occur  
            //    return ???? ; 

            return NoContent();
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _service.Delete(id);

            if (!res)
                return NotFound();

            return NoContent();

        }
        #endregion

    }
}
