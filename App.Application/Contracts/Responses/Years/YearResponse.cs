
namespace App.Application.Contracts.Responses.Years;

public record YearResponse
(
    int Id,
    string Name,
    DateTime StartDate,
    DateTime EndDate
);

