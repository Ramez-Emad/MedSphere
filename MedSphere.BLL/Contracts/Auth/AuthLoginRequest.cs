namespace MedSphere.BLL.Contracts.Auth;

public record AuthLoginRequest
(
    string Email,
    string Password
    );

