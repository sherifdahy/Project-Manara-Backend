using App.Application.Contracts.Responses.Days;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Days;

public record GetAllDaysQuery : IRequest<Result<List<DayResponse>>>
{

}
