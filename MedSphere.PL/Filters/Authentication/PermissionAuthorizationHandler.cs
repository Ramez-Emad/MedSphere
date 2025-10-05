using MedSphere.BLL.Consts;
using Microsoft.AspNetCore.Authorization;

namespace MedSphere.PL.Filters.Authentication;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User.Identity is not { IsAuthenticated: true } ||
           !context.User.Claims.Any(x => x.Type == Permissions.Type && x.Value == requirement.Permission))
            return Task.CompletedTask;

        context.Succeed(requirement);
        return Task.CompletedTask;
    }

}
