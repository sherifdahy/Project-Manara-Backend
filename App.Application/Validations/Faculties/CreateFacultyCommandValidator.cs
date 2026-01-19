using App.Application.Commands.Faculties;

namespace App.Application.Validations.Faculties;

public class CreateFacultyCommandValidator : AbstractValidator<CreateFacultyCommand>
{
    public CreateFacultyCommandValidator()
    {
        RuleFor(f => f.UniversityId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(f => f.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(f => f.Description)
            .NotEmpty()
            .MaximumLength(1000);


        RuleFor(f => f.Address)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(f => f.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(f => f.Website)
            .NotEmpty()
            .MaximumLength(200);

    }
}
