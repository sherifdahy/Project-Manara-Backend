using App.Application.Constants;
using App.Application.Contracts.Requests.DepartmentUsers;

namespace App.Application.Validations.DepartmentUsers;

public class DepartmentUserRequestValidator : AbstractValidator<DepartmentUserRequest>
{
    public DepartmentUserRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().Matches(RegexPatterns.Password);
        RuleFor(x => x.SSN).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Roles).NotEmpty().Must(r => r.Distinct().Count() == r.Count());
    }

}

