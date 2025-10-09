using Mapster;
using MedSphere.BLL.Abstractions;
using MedSphere.BLL.Consts;
using MedSphere.BLL.Contracts.Roles;
using MedSphere.BLL.Errors.Roles;
using MedSphere.DAL.Entities.Auth;
using MedSphere.DAL.Repositories.RoleClaims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedSphere.BLL.Services.Roles;

public class RoleService(RoleManager<ApplicationRole> _roleManager , IRoleClaimRepository _roleClaimRepository) : IRoleService
{
    #region GetAll Roles
    public async Task<Result<IEnumerable<RoleResponse>>> GetAllRolesAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _roleManager.Roles
                                      .Where(r => !r.IsDeleted)
                                      .ToListAsync(cancellationToken);

        var response = roles.Adapt<IEnumerable<RoleResponse>>();

        return Result.Success(response);
    }



    #endregion

    #region Get Role 
    public async Task<Result<RoleDetailResponse>> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken = default)
    {
        if (await _roleManager.FindByIdAsync(roleId) is not { } role || role.IsDeleted )
            return Result.Failure<RoleDetailResponse>(RoleErrors.RoleNotFound);

        var claims = await _roleManager.GetClaimsAsync(role);

        var permissions = claims.Where(c => c.Type == Permissions.Type).Select(c => c.Value).ToList();

        return Result.Success(new RoleDetailResponse(role.Id, role.Name!, permissions));

    }

    #endregion
    
    #region Add Role

    public async Task<Result<RoleDetailResponse>> AddRoleAsync(RoleRequest request, CancellationToken cancellationToken = default)
    {
        #region check if role already exists
        
        if (await _roleManager.RoleExistsAsync(request.Name))
            return Result.Failure<RoleDetailResponse>(RoleErrors.DuplicatedRole);

        #endregion

        #region Check Permessions are Exist
        var allowedPermissions = Permissions.GetAllPermissions();

        if (request.Permissions.Except(allowedPermissions).Any())
            return Result.Failure<RoleDetailResponse>(RoleErrors.InvalidPermissions);

        #endregion

        #region create new role
        
        var role = new ApplicationRole
        {
            Name = request.Name,
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        var result = await _roleManager.CreateAsync(role);

        #endregion

        #region Check Error while Adding

        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure<RoleDetailResponse>(new Error(
                                                                error.Code,
                                                                error.Description,
                                                                StatusCodes.Status400BadRequest)
                );

        }

        #endregion

        #region Assign permissions to role
        
        var permissions = request.Permissions.Select(p => new IdentityRoleClaim<string>
        {
            ClaimType = Permissions.Type,
            ClaimValue = p,
            RoleId = role.Id
        }).ToList();

        

        await _roleClaimRepository.AddRangeAsync(permissions);
        await _roleClaimRepository.SaveChangesAsync(cancellationToken);

        #endregion
       
        return Result.Success(new RoleDetailResponse(role.Id, role.Name, request.Permissions));
    }

    #endregion
 
    #region Update Role
    public async Task<Result> UpdateRoleAsync(string id, RoleRequest request, CancellationToken cancellationToken = default)
    {

        #region Check Role Exist

        if (await _roleManager.FindByIdAsync(id) is not { } role || role.IsDeleted)
            return Result.Failure<RoleDetailResponse>(RoleErrors.RoleNotFound);

        #endregion

        #region Check Permessions are Exist

        var allowedPermissions = Permissions.GetAllPermissions();

        if (request.Permissions.Except(allowedPermissions).Any())
            return Result.Failure<RoleDetailResponse>(RoleErrors.InvalidPermissions);

        #endregion

        #region Update Name and Check Duplicate Role Name

        if (role.Name != request.Name)
        {
            if ((await _roleManager.FindByNameAsync(request.Name) is not null))
                return Result.Failure<RoleDetailResponse>(RoleErrors.DuplicatedRole);
            else
            {
                role.Name = request.Name;
                var result = await _roleManager.UpdateAsync(role);

                if (!result.Succeeded)
                {
                    var error = result.Errors.First();
                    return Result.Failure<RoleDetailResponse>(new Error(
                                                                        error.Code,
                                                                        error.Description,
                                                                        StatusCodes.Status400BadRequest)
                        );

                }
            }
        }

        #endregion

        #region Update role permissions

        
        var oldPermissions = await _roleClaimRepository.GetAllRoleClaimsById(role.Id);
        var oldPermissionValues = oldPermissions.Where(c => c.ClaimType == Permissions.Type).Select(c => c.ClaimValue).ToList();

        #region Remove Permissions

        var neededToRemove = oldPermissions.Where(c => c.ClaimType == Permissions.Type
                                                && !request.Permissions.Contains(c.ClaimValue!));

        if (neededToRemove.Any() )
            _roleClaimRepository.RemoveRange(neededToRemove);

        #endregion

        #region Add New Permissions

        var addedPermissions = request.Permissions.Except(oldPermissionValues);
        if (addedPermissions.Any())
        {
            var addedClaims = addedPermissions.Select(p => new IdentityRoleClaim<string>
            {
                ClaimType = Permissions.Type,
                ClaimValue = p,
                RoleId = role.Id
            }).ToList();

            await _roleClaimRepository.AddRangeAsync(addedClaims);
        }
        #endregion
      
        await _roleClaimRepository.SaveChangesAsync(cancellationToken);

        #endregion

        return Result.Success();
    }

    #endregion
 
    #region Toggle Role
    public async Task<Result> ToggleRoleAsync(string id, CancellationToken cancellationToken = default)
    {
        if ( await _roleManager.FindByIdAsync(id) is not { } role )
            return Result.Failure(RoleErrors.RoleNotFound);

        role.IsDeleted = !role.IsDeleted;
        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code,error.Description,StatusCodes.Status400BadRequest));
        }
    
        return Result.Success();
    }

    #endregion

}
