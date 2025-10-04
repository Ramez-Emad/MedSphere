using MedSphere.BLL.Contracts.Medicines;
using MedSphere.BLL.Services.Medicines;
using MedSphere.PL.Extensions;
using Microsoft.AspNetCore.Authorization;
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
                         : result.ToProblem();
        }

        #endregion
       
        #region Create
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(MedicineRequest medicine)
        {
            var result = await _service.AddAsync(medicine);

            return result.IsSuccess
                         ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
                         : result.ToProblem();

        }

        #endregion

        #region Update
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Edit(int id, MedicineRequest medicine)
        {
            var result = await _service.Update(id, medicine);

            return result.IsSuccess
                         ? NoContent()
                         : result.ToProblem();
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            return result.IsSuccess
                         ? NoContent()
                         : result.ToProblem();

        }
        #endregion
    }
}
