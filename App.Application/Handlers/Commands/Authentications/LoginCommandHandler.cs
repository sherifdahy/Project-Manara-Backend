using App.Application.Contracts.Responses.Authentications;
using App.Core.Entities.Identity;
using App.Infrastructure.Abstractions.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace App.Application.Handlers.Commands.Authentications;

public class LoginCommandHandler(UserManager<ApplicationUser> userManager
    ,SignInManager<ApplicationUser> signInManager
    ,IJwtProvider jwtProvider,
    IAuthenticationService authenticationService,AuthenticationErrors errors,
    RoleManager<ApplicationRole> roleManager) : IRequestHandler<LoginCommand, Result<AuthenticationResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly AuthenticationErrors _errors = errors;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    public async Task<Result<AuthenticationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = _userManager.Users
            .Include(w => w.RefreshTokens)
            .Include(s => s.PermissionOverrides)
            .FirstOrDefault(x => x.Email == request.Email);

        if (user is null)
            return Result.Failure<AuthenticationResponse>(_errors.InvalidCredentials);

        if (user.IsDisabled)
            return Result.Failure<AuthenticationResponse>(_errors.DisabledUser);

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);

        if (result.Succeeded)
        {
            var (roles, permissions) = await _authenticationService.GetUserOverrideRolesAndPermissions(user, cancellationToken);

            var (token, expiresIn) = _jwtProvider.GenerateToken(user, roles, permissions);

            var (refreshToken, refreshTokenExpiration) = _authenticationService.AddRefreshToken(user);

            await _userManager.UpdateAsync(user);

            var response = new AuthenticationResponse(user.Id, user.Email, user.Name,token, expiresIn, refreshToken, refreshTokenExpiration);

            return Result.Success<AuthenticationResponse>(response);

        }

        var error = result.IsNotAllowed
            ? _errors.EmailNotConfirmed
            : result.IsLockedOut
            ? _errors.LockedUser
            : _errors.InvalidCredentials;

        return Result.Failure<AuthenticationResponse>(error);
    }
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
