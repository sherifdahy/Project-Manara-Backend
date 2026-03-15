using App.Application.Contracts.Responses.Departments;
using App.Application.Queries.Departments;

namespace App.Application.Handlers.Queries.Departments;

public class GetDepartmentQueryHandler(IUnitOfWork unitOfWork,DepartmentErrors departmentErrors) : IRequestHandler<GetDepartmentQuery, Result<DepartmentDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly DepartmentErrors _departmentErrors = departmentErrors;

    public async Task<Result<DepartmentDetailResponse>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
    {
        var department = await _unitOfWork.Departments.FindAsync(x => x.Id == request.Id, null, cancellationToken);

        if (department == null)
            return Result.Failure<DepartmentDetailResponse>(_departmentErrors.NotFound);

        var response = department.Adapt<DepartmentDetailResponse>();

        return Result.Success(response);

    }

}