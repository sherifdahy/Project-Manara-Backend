using App.Application.Abstractions;
using App.Application.Responses.Authentications;
using App.Application.Responses.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Authentications;

public record LoginCommand : IRequest<Result<AuthenticationResponse>>
{
    public const string Route = "login";
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
