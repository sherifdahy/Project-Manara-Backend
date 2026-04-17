using App.Application.Contracts.Requests.Enrollments;

namespace App.Application.Validations.Enrollments;

public class EnrollmentRequestValidator : AbstractValidator<EnrollmentRequest>
{
    public EnrollmentRequestValidator()
    {
        RuleFor(f => f.ProgramId)
            .NotEmpty()
            .NotNull();

        RuleFor(f => f.YearId)
            .NotEmpty()
            .NotNull();

        RuleFor(f => f.TermId)
            .NotEmpty()
            .NotNull();

    }
}
