using App.Application.Commands.Authentications;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.Authentications;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty();

        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}
