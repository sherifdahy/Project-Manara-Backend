

using App.Application.Contracts.Requests.Departments;
using App.Application.Contracts.Requests.Subjects;

namespace App.Application.Validations.Subjects;

public class SubjectRequestValidator : AbstractValidator<SubjectRequest>
{
    public SubjectRequestValidator()
    {
        RuleFor(f => f.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(2)
            .MaximumLength(200);

        RuleFor(f => f.Code)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(200);

        RuleFor(f => f.Description)
            .NotEmpty()
            .NotNull()
            .MaximumLength(1000);


        RuleFor(f => f.CreditHours)
            .NotEmpty()
            .NotNull();

    }
}

