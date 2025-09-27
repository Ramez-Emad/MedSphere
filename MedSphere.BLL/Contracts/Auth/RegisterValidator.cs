using MedSphere.Shared.Const;

namespace MedSphere.BLL.Contracts.Auth;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .Length(2, 50)
            .WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .Length(2, 50)
            .WithMessage("Last name must be between 2 and 50 characters.");

        RuleFor(req => req.Password)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage("Password should be at least 8 digits and should contains Lowercase, Non Alphanumeric and Uppercase");

    }
}
