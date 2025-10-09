using Azure.Core;
using MedSphere.BLL.Consts;
using MedSphere.BLL.Contracts.Roles;
using MedSphere.BLL.Services.Roles;
using MedSphere.PL.Extensions;
using MedSphere.PL.Filters.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedSphere.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(IRoleService _roleService) : ControllerBase
{
    #region GetAll

    [HttpGet]
    [HasPermission(Permissions.GetRoles)]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _roleService.GetAllRolesAsync(cancellationToken); 

        return result. IsSuccess
                     ? Ok(result.Value)
                     : result.ToProblem();
    }
    #endregion

    #region Get Role
    [HttpGet("{id}")]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
    {
        var result = await _roleService.GetRoleByIdAsync(id, cancellationToken);

        return result.IsSuccess
                     ? Ok(result.Value)
                     : result.ToProblem();
    }

    #endregion

    #region Add Role
   
    [HttpPost]
    [HasPermission(Permissions.AddRoles)]
    public async Task<IActionResult> Create(RoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _roleService.AddRoleAsync(request, cancellationToken);

        return result.IsSuccess
                     ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
                     : result.ToProblem();
    }

    #endregion

    #region Update Role

    [HttpPost("{id}")]
    [HasPermission(Permissions.UpdateRoles)]
    public async Task<IActionResult> Update(string id, RoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _roleService.UpdateRoleAsync(id, request, cancellationToken);

        return result.IsSuccess
                     ? NoContent()
                     : result.ToProblem();
    }
    #endregion

    #region Toggle Role

    [HttpPost("delete/{id}")]
    [HasPermission(Permissions.DeleteRoles)]
    public async Task<IActionResult> Toggle(string id, CancellationToken cancellationToken)
    {
        var result = await _roleService.ToggleRoleAsync(id, cancellationToken);

        return result.IsSuccess
                     ? NoContent()
                     : result.ToProblem();

    }

    #endregion
}
