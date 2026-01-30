namespace App.Application.Contracts.Responses.Faculties;

public record FacultyDetailResponse
(
    string Name,
    string Description,
    string Address,
    string Email,
    string Website
);
