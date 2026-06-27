using App.Application.Contracts.Requests.Departments;
using App.Application.Contracts.Requests.StudentPortals;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.StudentPortals;

public class RegisterLectureRequestValidator : AbstractValidator<RegisterLectureRequest>
{
    public RegisterLectureRequestValidator()
    {
        RuleFor(f => f.LectureScheduleId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

    }
}
