using MedSphere.DAL.Entities.Auth;

namespace MedSphere.BLL.Services.Auth.Jwt;

public interface IJwtProvider
{
    (string token, int expiresIn) GenerateToken(ApplicationUser applicationUser);
    public string? ValidateToken(string token);
}
