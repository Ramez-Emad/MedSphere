using MedSphere.BLL.Abstractions;
using MedSphere.BLL.Contracts.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Services.Roles;

public interface IRoleService
{
    Task<Result<RoleDetailResponse>> AddRoleAsync(RoleRequest request, CancellationToken cancellationToken = default);
    Task<Result<RoleDetailResponse>> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken = default);
}
