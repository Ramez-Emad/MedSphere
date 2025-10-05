using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace MedSphere.PL.Filters.Authentication;

public class PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
{
    private readonly AuthorizationOptions _authorizationOptions = options.Value;

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if( await base.GetPolicyAsync(policyName) is { } policy)
            return policy;

        var permissionPolicy = new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(policyName))
            .Build();   

        _authorizationOptions.AddPolicy(policyName, permissionPolicy);

        return permissionPolicy;
    }

}
