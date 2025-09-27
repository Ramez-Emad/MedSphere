namespace MedSphere.BLL.Contracts.Auth;

public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");

        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("Token is required.");
    }
}
