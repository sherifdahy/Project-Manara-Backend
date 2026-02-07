using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.FacultyUsers;

public record UpdateFacultyUserCommand : IRequest<Result>
{
    public string Name { get; set; } = string.Empty;
    public string SSN { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }
    public List<string> Roles { get; set; } = [];
    public int UserId { get; set; }
}
