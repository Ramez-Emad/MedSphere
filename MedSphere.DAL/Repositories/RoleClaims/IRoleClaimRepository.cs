using MedSphere.DAL.Repositories._Generic;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.DAL.Repositories.RoleClaims;

public interface IRoleClaimRepository
{
    Task AddRangeAsync(IEnumerable<IdentityRoleClaim<string>> identityRoleClaims);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
