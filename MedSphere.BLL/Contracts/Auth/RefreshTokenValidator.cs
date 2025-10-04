namespace MedSphere.BLL.Contracts.Auth;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.token)
            .NotEmpty()
            .WithMessage("Token is required");
        RuleFor(x => x.refreshToken)
            .NotEmpty()
            .WithMessage("Refresh token is required");
    }
}
