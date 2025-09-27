namespace MedSphere.BLL.Contracts.Auth;

public class AuthLoginValidator : AbstractValidator<AuthLoginRequest>
{
    public AuthLoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}