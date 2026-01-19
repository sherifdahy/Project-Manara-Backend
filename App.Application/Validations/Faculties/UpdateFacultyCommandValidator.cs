using App.Application.Commands.Faculties;

namespace App.Application.Validations.Faculties;

public class UpdateFacultyCommandValidator : AbstractValidator<UpdateFacultyCommand>
{
    public UpdateFacultyCommandValidator()
    {
        RuleFor(f => f.Id)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(f => f.UniversityId)
            .GreaterThan(0);

        RuleFor(f => f.Name)
            .NotEmpty()
            .MaximumLength(200);


        RuleFor(f => f.Description)
            .MaximumLength(1000);


        RuleFor(f => f.Address)
            .MaximumLength(200);

        RuleFor(f => f.Email)
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(f => f.Website)
            .MaximumLength(200);
    }
}
