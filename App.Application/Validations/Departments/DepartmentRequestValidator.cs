using App.Application.Contracts.Requests.Departments;
using App.Application.Contracts.Requests.Faculties;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.Departments;

public class DepartmentRequestValidator : AbstractValidator<DepartmentRequest>
{
    public DepartmentRequestValidator()
    {
        RuleFor(f => f.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(200);

        RuleFor(f => f.Code)
            .NotEmpty()
            .NotNull()
            .MaximumLength(200);

        RuleFor(f => f.Description)
            .NotEmpty()
            .NotNull()
            .MaximumLength(1000);

        RuleFor(f => f.HeadOfDepartment)
            .NotEmpty()
            .NotNull()
            .MaximumLength(200);

        RuleFor(f => f.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .MaximumLength(200);


    }
}
