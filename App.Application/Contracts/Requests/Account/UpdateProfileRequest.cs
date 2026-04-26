namespace App.Application.Contracts.Requests.Account;

public record UpdateProfileRequest
{
    public string PhoneNumber { get; set; } = string.Empty;
}
