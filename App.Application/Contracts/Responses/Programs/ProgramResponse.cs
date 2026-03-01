namespace App.Application.Contracts.Responses.Programs;

public record ProgramResponse
(
    int Id,
    string Name,
    string Code,
    string Description,
    int CreditHours,
    bool IsDeleted,
    int DepartmentId
);
