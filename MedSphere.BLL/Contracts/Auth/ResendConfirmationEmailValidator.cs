using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
