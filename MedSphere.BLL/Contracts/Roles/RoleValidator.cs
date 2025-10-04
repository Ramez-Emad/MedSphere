using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Contracts.Roles;

public class RoleValidator : AbstractValidator<RoleRequest>
{
    public RoleValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Role name is required.")
            .Length(3, 200);

        RuleFor(x => x.Permissions)
            .NotNull()
            .NotEmpty()
            .Must(p => p.Distinct().Count() == p.Count)
            .WithMessage("You cannot add duplicated permissions for the same role");

    }
}
