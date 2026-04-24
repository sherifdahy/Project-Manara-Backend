using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Account;

public record UpdateProfileCommand : IRequest<Result>
{
    public string PhoneNumber { get; set; } = string.Empty;
}
