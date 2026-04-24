using App.Application.Constants;
using App.Application.Contracts.Requests.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.Account;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithMessage("Current password is required.");

        RuleFor(x => x.NewPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("New password is required.")
            .NotEqual(x => x.CurrentPassword)
                .WithMessage("New password must be different from current password.")
            .Matches(RegexPatterns.Password)
                .WithMessage("Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.");
    }
}