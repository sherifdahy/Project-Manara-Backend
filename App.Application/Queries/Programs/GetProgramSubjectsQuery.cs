using App.Application.Contracts.Responses.Subjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Programs;

public record GetProgramSubjectsQuery : IRequest<Result<List<SubjectResponse>>>
{
    public int ProgramId { get; set; }
}
