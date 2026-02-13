using App.Application.Commands.Departments;
using App.Application.Commands.Programs;
using App.Application.Errors;
using App.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Commands.Programs;

public class ToggleStatusProgramCommandHandler(IUnitOfWork unitOfWork, ProgramErrors programErrors) : IRequestHandler<ToggleStatusProgramCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramErrors _programErrors = programErrors;


    public async Task<Result> Handle(ToggleStatusProgramCommand request, CancellationToken cancellationToken)
    {
        var program = await _unitOfWork.Programs.GetByIdAsync(request.Id);

        if (program is null)
            return Result.Failure(_programErrors.NotFound);

        program.IsDeleted = !program.IsDeleted;

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();

    }
}