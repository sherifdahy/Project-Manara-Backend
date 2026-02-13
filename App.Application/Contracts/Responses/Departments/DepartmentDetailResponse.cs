using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Departments;

public record DepartmentDetailResponse
(
    int Id,
    string Name,
    string Code,
    string Description,
    string HeadOfDepartment,
    string Email,
    int FacultyId
);
