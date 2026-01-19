using App.Application.Responses.Faculties;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Responses.Universities;

public record UniversityResponse
(
    int Id,
    string Name,
    string Description,
    string Address,
    string Email,
    string Website,
    bool IsDeleted
);