using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.UniversityUsers;

public record ToggleStatusUniversityUserCommand : IRequest<Result>
{
    public int Id { get; set; }
}

