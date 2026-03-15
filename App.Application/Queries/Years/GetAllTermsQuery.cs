using App.Application.Contracts.Responses.Years;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Years;

public record GetAllTermsQuery : IRequest<Result<List<TermResponse>>> { }

