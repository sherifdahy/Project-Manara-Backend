using App.Application.Contracts.Requests.StudentPortals;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.StudentPortals;

public class UpdateStudentGradeRequestValidator : AbstractValidator<UpdateStudentGradeRequest>
{
    public UpdateStudentGradeRequestValidator()
    {
        RuleFor(f => f.StudentId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

        RuleFor(f => f.GPA)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0)
            .LessThanOrEqualTo(4);
    }
}

