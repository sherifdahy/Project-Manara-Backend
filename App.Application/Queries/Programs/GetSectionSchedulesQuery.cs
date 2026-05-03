using App.Application.Contracts.Responses.Programs;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Programs;

public record GetSectionSchedulesQuery
(
    int ProgramId
) : IRequest<Result<List<SectionScheduleItemResponse>>>;
