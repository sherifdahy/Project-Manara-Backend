using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Account;

public record ChangePasswordCommand : IRequest<Result>
{
    public string NewPassword { get; set; } = string.Empty;
    public string CurrentPassword { get; set; } = string.Empty;
}
