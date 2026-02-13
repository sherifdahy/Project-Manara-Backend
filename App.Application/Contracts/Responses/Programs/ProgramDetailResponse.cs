using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Programs;

public record ProgramDetailResponse
(
    int Id,
    string Name,
    string Code,
    string Description,
    int CreditHours,
    int DepartmentId
);
