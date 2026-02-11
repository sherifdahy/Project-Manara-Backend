using App.Application.Contracts.Responses.Faculties;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Faculties;

public record GetMyFacultQuery : IRequest<Result<FacultyDetailResponse>> { }
