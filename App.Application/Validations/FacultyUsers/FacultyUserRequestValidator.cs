using App.Application.Constants;
using App.Application.Contracts.Requests.FacultyUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.FacultyUsers;

public class FacultyUserRequestValidator : AbstractValidator<FacultyUserRequest>
{
    public FacultyUserRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password)
            .Matches(RegexPatterns.Password)
            .When(x => !string.IsNullOrWhiteSpace(x.Password));
        RuleFor(x => x.SSN).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Roles).NotEmpty().Must(r => r.Distinct().Count() == r.Count());
    }

}
