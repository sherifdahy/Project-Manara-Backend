using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Enrollments;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Enrollments;

public record GetEnrollmentQuery(int id) : IRequest<Result<EnrollmentDetailResponse>>;
