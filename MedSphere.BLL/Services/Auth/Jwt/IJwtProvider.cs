using MedSphere.DAL.Entities.Auth;

namespace MedSphere.BLL.Services.Auth.Jwt;

public interface  IJwtProvider
{
    (string token, int expiresIn) GenerateJwtToken(ApplicationUser applicationUser, IEnumerable<string> roles, IEnumerable<string> permissions);
    public string? ValidateToken(string token);
}
