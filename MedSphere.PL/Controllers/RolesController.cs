using MedSphere.BLL.Contracts.Roles;
using MedSphere.BLL.Services.Roles;
using MedSphere.PL.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedSphere.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(IRoleService _roleService) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id , CancellationToken cancellationToken)
    {   
        var result = await _roleService.GetRoleByIdAsync(id, cancellationToken);

        return result.IsSuccess
                     ? Ok(result.Value)
                     : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _roleService.AddRoleAsync(request, cancellationToken);

        return result.IsSuccess
                     ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
                     : result.ToProblem();
    }
}
