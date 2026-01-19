namespace App.Application.Responses.Faculties;

public record FacultyDetailResponse
(
    string Name,
    string Description,
    string Address,
    string Email,
    string Website
);
