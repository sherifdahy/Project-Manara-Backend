

namespace App.Application.Contracts.Responses.Subjects;

public record SubjectResponse
(
    int Id,
   string Name,
   string Code,
   string Description,
   int CreditHours,
   bool IsDeleted
);

