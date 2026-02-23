using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.UniversityUsers;

public record UniversityUserRequest
{
    public string Name { get; set; } = string.Empty;
    public string SSN { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Password { get; set; }
    public bool IsDisabled { get; set; } = false;
    public List<string> Roles { get; set; } = [];
}
