namespace MedSphere.BLL.Contracts.Auth;

public record ConfirmEmailRequest(
    string UserId,
    string Token
    );