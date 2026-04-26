using App.Application.Constants;
using App.Application.Contracts.Requests.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.Account;

public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileRequestValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(RegexPatterns.EgyptianPhoneNumber)
                .WithMessage("Phone number must be a valid Egyptian number.");
    }
}
