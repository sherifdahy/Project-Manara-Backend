using App.Application.Contracts.Responses.DepartmentUsers;
using App.Core.Entities.Personnel;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.DepartmentUsers;

public record GetDoctorsQuery(int DepartmentId,RequestFilters filters) : IRequest<Result<PaginatedList<DepartmentUserResponse>>>;
