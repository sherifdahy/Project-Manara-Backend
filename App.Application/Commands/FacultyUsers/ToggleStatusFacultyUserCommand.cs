using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.FacultyUsers;

public record ToggleStatusFacultyUserCommand : IRequest<Result>
{
    public int Id { get; set; }
}
