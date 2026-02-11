using App.Application.Commands.Departments;
using App.Application.Commands.Faculties;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Commands.Departments;

public class ToggleStatusDepartmentCommandHandler(IUnitOfWork unitOfWork,DepartmentErrors departmentErrors) : IRequestHandler<ToggleStatusDepartmentCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly DepartmentErrors _departmentErrors = departmentErrors;

    public async Task<Result> Handle(ToggleStatusDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _unitOfWork.Departments.GetByIdAsync(request.Id);

        if (department is null)
            return Result.Failure(_departmentErrors.NotFound);

        
        department.IsDeleted = !department.IsDeleted;

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
