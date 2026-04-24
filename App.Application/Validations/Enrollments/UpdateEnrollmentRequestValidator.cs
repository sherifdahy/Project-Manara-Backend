using App.Application.Contracts.Requests.Enrollments;

namespace App.Application.Validations.Enrollments;

public class UpdateEnrollmentRequestValidator : AbstractValidator<UpdateEnrollmentRequest>
{
    public UpdateEnrollmentRequestValidator()
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
