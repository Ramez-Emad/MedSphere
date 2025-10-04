using Microsoft.AspNetCore.Identity;

namespace MedSphere.DAL.Repositories.RoleClaims;

public interface IRoleClaimRepository
{
    Task<IEnumerable<string>> GetAllRoleClaimsById (string roleId);
    Task AddRangeAsync(IEnumerable<IdentityRoleClaim<string>> identityRoleClaims);
    void RemoveRange(string roleId, IEnumerable<string> permissionsToRemove);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
