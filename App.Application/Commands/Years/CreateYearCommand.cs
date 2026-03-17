

using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Years;

namespace App.Application.Commands.Years;

public record CreateYearCommand : IRequest<Result<YearResponse>>
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ActiveTermId { get; set; }
    public int FacultyId { get; set; }
}
