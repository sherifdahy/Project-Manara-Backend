using App.Application.Abstractions;
using App.Application.Responses.Authentications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Authentications;

public record RegisterCommand  : IRequest<Result<AuthenticationResponse>>
{
    public const string Route = "register";
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
