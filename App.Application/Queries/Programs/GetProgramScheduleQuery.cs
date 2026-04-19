using App.Application.Contracts.Responses.ProgramSchedules;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Programs;

public record GetProgramScheduleQuery : IRequest<Result<ProgramScheduleResponse>>
{
    public int ProgramId { get; set; }
}
