using Microsoft.AspNetCore.Identity;

namespace MedSphere.DAL.Repositories.RoleClaims;

public interface IRoleClaimRepository
{
    Task<IEnumerable<IdentityRoleClaim<string>>> GetAllRoleClaimsById (string roleId);
    Task AddRangeAsync(IEnumerable<IdentityRoleClaim<string>> identityRoleClaims);
    void RemoveRange(IEnumerable<IdentityRoleClaim<string>> identityRoleClaims);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<string>> GetClaimsByRolesNameAsync(IEnumerable<string> roles);
}
