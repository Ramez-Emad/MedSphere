using Microsoft.AspNetCore.Authorization;

namespace MedSphere.PL.Filters.Authentication;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}
