using MedSphere.BLL.Abstractions;
using MedSphere.BLL.Consts;
using MedSphere.BLL.Contracts.Roles;
using MedSphere.BLL.Errors.Roles;
using MedSphere.DAL.Entities.Auth;
using MedSphere.DAL.Repositories.RoleClaims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Services.Roles;

public class RoleService(RoleManager<ApplicationRole> _roleManager , IRoleClaimRepository _roleClaimRepository) : IRoleService
{
    public async Task<Result<RoleDetailResponse>> AddRoleAsync(RoleRequest request, CancellationToken cancellationToken = default)
    {
        // check if role already exists
        if( await _roleManager.RoleExistsAsync(request.Name))
            return Result.Failure<RoleDetailResponse>(RoleErrors.DuplicatedRole);


        // check Permissions

        var allowedPermissions = Permissions.GetAllPermissions();

        if (request.Permissions.Except(allowedPermissions).Any())
            return Result.Failure<RoleDetailResponse>(RoleErrors.InvalidPermissions);



        // create new role

        var role = new ApplicationRole
        {
            Name = request.Name
        };

        var result = await _roleManager.CreateAsync(role);

        if(!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure<RoleDetailResponse>(new Error(
                                                                error.Code,
                                                                error.Description,
                                                                StatusCodes.Status400BadRequest)
                );

        }
        // Assign permissions to role

        var permissions = request.Permissions.Select(p => new IdentityRoleClaim<string>
        {
            ClaimType = Permissions.Type,
            ClaimValue = p,
            RoleId = role.Id
        }).ToList();

        await _roleClaimRepository.AddRangeAsync(permissions);
        await _roleClaimRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(new RoleDetailResponse(role.Id, role.Name, request.Permissions));

    }

    public async Task<Result<RoleDetailResponse>> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken = default)
    {
        if (await _roleManager.FindByIdAsync(roleId) is not { } role)
            return Result.Failure<RoleDetailResponse>(RoleErrors.RoleNotFound);

        var claims = await _roleManager.GetClaimsAsync(role);
        
        var permissions = claims.Where(c => c.Type == Permissions.Type).Select(c => c.Value).ToList();

        return Result.Success(new RoleDetailResponse(role.Id, role.Name!, permissions));

    }
}
