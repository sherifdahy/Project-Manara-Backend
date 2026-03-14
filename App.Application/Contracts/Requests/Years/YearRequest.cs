

namespace App.Application.Contracts.Requests.Years;

public record YearRequest
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ActiveTermId { get; set; }
}
