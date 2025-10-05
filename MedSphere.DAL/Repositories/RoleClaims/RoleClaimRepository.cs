using MedSphere.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MedSphere.DAL.Repositories.RoleClaims;

public class RoleClaimRepository(AppDbContext appDbContext) : IRoleClaimRepository
{
    
    public async Task<IEnumerable<IdentityRoleClaim<string>>> GetAllRoleClaimsById(string roleId)
        => await appDbContext.RoleClaims.Where(R => R.RoleId == roleId).ToListAsync();

    public async Task AddRangeAsync(IEnumerable<IdentityRoleClaim<string>> identityRoleClaims)
        => await appDbContext.AddRangeAsync(identityRoleClaims);
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
         => await appDbContext.SaveChangesAsync(cancellationToken);

    public void RemoveRange(IEnumerable<IdentityRoleClaim<string>> identityRoleClaims)
        => appDbContext.RemoveRange(identityRoleClaims);
    
}
