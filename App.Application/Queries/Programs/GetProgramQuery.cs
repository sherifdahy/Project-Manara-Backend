using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Programs;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Programs;

public record GetProgramQuery(int Id) : IRequest<Result<ProgramDetailResponse>>;