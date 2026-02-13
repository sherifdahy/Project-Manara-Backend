using App.Application.Commands.Departments;
using App.Application.Commands.Faculties;
using App.Application.Errors;
using App.Infrastructure.Repository;
using SA.Accountring.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Commands.Departments;

public class UpdateDepartmentCommandHandler(IUnitOfWork unitOfWork,DepartmentErrors departmentErrors) : IRequestHandler<UpdateDepartmentCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly DepartmentErrors _departmentErrors = departmentErrors;

    public async Task<Result> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {

        var department = await _unitOfWork.Departments.GetByIdAsync(request.Id, cancellationToken);

        if (department == null)
            return Result.Failure(_departmentErrors.NotFound);

        if (await _unitOfWork.Departments.IsExistAsync(x => x.FacultyId == department.FacultyId && x.Name == request.Name && x.Id != request.Id))
            return Result.Failure(_departmentErrors.DuplicatedName);

        request.Adapt(department);

        _unitOfWork.Departments.Update(department);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
