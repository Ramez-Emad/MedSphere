using Microsoft.AspNetCore.Identity;

namespace MedSphere.DAL.Entities.Auth;

public class ApplicationRole : IdentityRole
{
    public bool IsDefault { get; set; } = false;
}
