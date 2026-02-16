using App.Application.Constants;
using App.Application.Contracts.Requests.UniversityUsers;

namespace App.Application.Validations.UinversityUsers;

public class UniversityUserRequestValidator : AbstractValidator<UniversityUserRequest>
{
    public UniversityUserRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().Matches(RegexPatterns.Password);
        RuleFor(x => x.SSN).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Roles).NotEmpty().Must(r => r.Distinct().Count() == r.Count());
    }

}
