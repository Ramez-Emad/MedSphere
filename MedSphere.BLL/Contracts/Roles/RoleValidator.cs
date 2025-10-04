namespace MedSphere.BLL.Contracts.Roles;

public class RoleValidator : AbstractValidator<RoleRequest>
{
    public RoleValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Role name is required.")
            .Length(3, 200)
            .WithMessage("Role Name at least 3 characters and at most 200 characters");

        RuleFor(x => x.Permissions)
            .NotNull()
            .NotEmpty()
            .WithMessage("You must enter at least 1 Role Permission.")
            .Must(p => p.Distinct().Count() == p.Count)
            .WithMessage("You cannot add duplicated permissions for the same role");

    }
}
