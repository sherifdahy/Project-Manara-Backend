using App.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.UniversityUsers;

public record UniversityUserRequest
{
    public string Name { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public Religion Religion { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsDisabled { get; set; } = false;
    public List<string> Roles { get; set; } = [];
}
