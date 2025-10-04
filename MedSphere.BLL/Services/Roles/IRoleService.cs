using MedSphere.BLL.Abstractions;
using MedSphere.BLL.Contracts.Roles;

namespace MedSphere.BLL.Services.Roles;

public interface IRoleService
{
    Task<Result<IEnumerable<RoleResponse>>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task<Result<RoleDetailResponse>> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken = default);
    Task<Result<RoleDetailResponse>> AddRoleAsync(RoleRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateRoleAsync(string id, RoleRequest request, CancellationToken cancellationToken = default);
    Task<Result> ToggleRoleAsync(string id, CancellationToken cancellationToken = default);


}
