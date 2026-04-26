using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.Account;

public record ChangePasswordRequest
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
