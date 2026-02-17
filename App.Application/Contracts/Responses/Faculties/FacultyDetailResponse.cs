namespace App.Application.Contracts.Responses.Faculties;

public record FacultyDetailResponse
(
    int Id,
    string Name,
    string Description,
    string Address,
    string Email,
    string Website
);
