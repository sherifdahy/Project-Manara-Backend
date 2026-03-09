namespace App.Application.Contracts.Responses.Subjects;

public record SubjectDetailResponse
(
   int Id,
   string Name,
   string Code,
   string Description,
   int CreditHours,
   bool IsDeleted,
   IEnumerable<SubjectPrerequisiteResponse> Prerequisites
);
