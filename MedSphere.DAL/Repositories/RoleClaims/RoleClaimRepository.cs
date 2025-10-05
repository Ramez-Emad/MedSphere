using MedSphere.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MedSphere.DAL.Repositories.RoleClaims;

public class RoleClaimRepository(AppDbContext appDbContext) : IRoleClaimRepository
{
    
    public async Task<IEnumerable<string>> GetAllRoleClaimsById(string roleId)
        => await appDbContext.RoleClaims.Where(R => R.RoleId == roleId).Select(x=>x.ClaimValue!).ToListAsync();

    public async Task AddRangeAsync(IEnumerable<IdentityRoleClaim<string>> identityRoleClaims)
        => await appDbContext.AddRangeAsync(identityRoleClaims);
    public void RemoveRange(string roleId, IEnumerable<string> permissionsToRemove)
    {
        var claimsToRemove = appDbContext.RoleClaims
                                    .Where(rc => rc.RoleId == roleId && permissionsToRemove.Contains(rc.ClaimValue!));
        appDbContext.RoleClaims.RemoveRange(claimsToRemove);
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
         => await appDbContext.SaveChangesAsync(cancellationToken);

}
