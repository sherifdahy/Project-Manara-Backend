using App.Application.Contracts.Responses.Faculties;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Universities;

public record UniversityDetailResponse
(
    int Id,
    string Name,
    string Description,
    string Address,
    string Email,
    string Website,
    int YearOfEstablishment,
    int NumberOfStudents,
    int NumberOfStuff,
    int NumberOfFacilities,
    List<FacultyResponse> Faculties
);
