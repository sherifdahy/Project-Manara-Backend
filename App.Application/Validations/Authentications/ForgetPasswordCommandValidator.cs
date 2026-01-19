

namespace App.Application.Validations.Authentications;

public class ForgetPasswordCommandValidator :AbstractValidator<ForgetPasswordCommand>
{
    public ForgetPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
