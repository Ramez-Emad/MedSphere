
namespace MedSphere.BLL.Contracts.Auth;

public class ResendConfirmationEmailValidator : AbstractValidator<ResendConfirmationEmailRequest>
{
    public ResendConfirmationEmailValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");
    }
}
