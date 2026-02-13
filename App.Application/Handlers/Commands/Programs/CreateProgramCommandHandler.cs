using App.Application.Commands.Departments;
using App.Application.Commands.Programs;
using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Programs;
using App.Application.Errors;
using App.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Commands.Programs;

public class CreateProgramCommandHandler(IUnitOfWork unitOfWork
    ,ProgramErrors programErrors
    ,DepartmentErrors departmentErrors) : IRequestHandler<CreateProgramCommand, Result<ProgramResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramErrors _programErrors = programErrors;
    private readonly DepartmentErrors _departmentErrors = departmentErrors;

    public async Task<Result<ProgramResponse>> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
    {
        var isDepartmentExists = _unitOfWork.Departments.IsExist(d => d.Id == request.DepartmentId);

        if (!isDepartmentExists)
            return Result.Failure<ProgramResponse>(_departmentErrors.NotFound);

        var isProgramExists = _unitOfWork.Programs
            .IsExist(x => x.DepartmentId == request.DepartmentId && x.Name == request.Name);

        if (isProgramExists)
            return Result.Failure<ProgramResponse>(_programErrors.DuplicatedName);

        var program = request.Adapt<Program>();

        await _unitOfWork.Programs.AddAsync(program);
        await _unitOfWork.SaveAsync();

        return Result.Success<ProgramResponse>(program.Adapt<ProgramResponse>());
    }
}
