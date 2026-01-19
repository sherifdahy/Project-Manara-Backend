namespace App.Application.Responses.Faculties;

public record FacultyResponse
(
    int Id,
    string Name,
    string Description,
    string Address,
    string Email,
    string Website,
    bool IsDeleted,
    int NumberOfStudents
);