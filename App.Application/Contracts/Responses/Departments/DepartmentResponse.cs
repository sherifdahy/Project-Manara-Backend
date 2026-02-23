namespace App.Application.Contracts.Responses.Departments;

public record DepartmentResponse 
(
    int Id,
    string Name,
    string Code,
    string Description,
    string HeadOfDepartment,
    string Email,
    bool IsDeleted,
    int FacultyId
);

