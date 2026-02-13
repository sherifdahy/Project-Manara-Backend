using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Programs;
using App.Application.Queries.Departments;
using App.Application.Queries.Programs;
using App.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.Programs;

public class GetAllProgramsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllProgramsQuery, Result<List<ProgramResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<ProgramResponse>>> Handle(GetAllProgramsQuery request, CancellationToken cancellationToken)
    {
        var departments = await _unitOfWork.Programs
            .FindAllAsync(x => x.DepartmentId == request.DepartmentId
                    && (!x.IsDeleted || (request.IncludeDisabled.HasValue && request.IncludeDisabled.Value)), null, cancellationToken);

        var response = departments.Adapt<List<ProgramResponse>>();
        return Result.Success(response);
    }
}

