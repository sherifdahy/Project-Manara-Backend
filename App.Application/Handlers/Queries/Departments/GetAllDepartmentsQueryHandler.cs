using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Faculties;
using App.Application.Queries.Departments;
using App.Application.Queries.Faculties;
using App.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.Departments;

public class GetAllDepartmentsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllDepartmentsQuery, Result<List<DepartmentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<DepartmentResponse>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = await _unitOfWork.Departments
            .FindAllAsync(x => x.FacultyId == request.FacultyId 
                && (!x.IsDeleted || (request.IncludeDisabled.HasValue && request.IncludeDisabled.Value)), null, cancellationToken);

        var response = departments.Adapt<List<DepartmentResponse>>();
        return Result.Success(response);
    }

}
