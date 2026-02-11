using App.Application.Contracts.Responses.Universities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Universities;

public record GetMyUniversityQuery() : IRequest<Result<UniversityDetailResponse>>;
