using App.Application.Commands.Departments;
using App.Application.Commands.Programs;
using App.Application.Errors;
using App.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Commands.Programs;

public class UpdateProgramCommandHandler(IUnitOfWork unitOfWork,ProgramErrors programErrors) : IRequestHandler<UpdateProgramCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramErrors _programErrors = programErrors;


    public async Task<Result> Handle(UpdateProgramCommand request, CancellationToken cancellationToken)
    {
        var program = await _unitOfWork.Programs.GetByIdAsync(request.Id, cancellationToken);

        if (program == null)
            return Result.Failure(_programErrors.NotFound);

        if (await _unitOfWork.Programs.IsExistAsync(x => x.DepartmentId == program.DepartmentId && x.Name == request.Name && x.Id != request.Id))
            return Result.Failure(_programErrors.DuplicatedName);

        request.Adapt(program);

        _unitOfWork.Programs.Update(program);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
