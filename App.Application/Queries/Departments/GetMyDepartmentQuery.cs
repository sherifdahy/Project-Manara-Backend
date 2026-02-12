using App.Application.Contracts.Responses.Departments;

namespace App.Application.Queries.Departments;

public class GetMyDepartmentQuery : IRequest<Result<DepartmentDetailResponse>> { }
