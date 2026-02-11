using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Faculties;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Departments;

public record GetDepartmentQuery(int Id) : IRequest<Result<DepartmentDetailResponse>>;
