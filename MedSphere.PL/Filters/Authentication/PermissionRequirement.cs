using Microsoft.AspNetCore.Authorization;

namespace MedSphere.PL.Filters.Authentication;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
