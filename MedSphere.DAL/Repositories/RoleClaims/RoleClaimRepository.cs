using MedSphere.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.DAL.Repositories.RoleClaims;

public class RoleClaimRepository(AppDbContext appDbContext) : IRoleClaimRepository
{
    public async Task AddRangeAsync(IEnumerable<IdentityRoleClaim<string>> identityRoleClaims)
        => await appDbContext.AddRangeAsync(identityRoleClaims);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
         => await appDbContext.SaveChangesAsync(cancellationToken);

}
