using App.Application.Contracts.Requests.Enrollments;

namespace App.Application.Validations.Enrollments;

public class CreateEnrollmentRequestValidator : AbstractValidator<CreateEnrollmentRequest>
{
    public CreateEnrollmentRequestValidator()
    {
        RuleFor(f => f.StudentIds)
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
