namespace App.Application.Handlers.Commands.Authentications;

public class RefreshTokenCommandHandler(IJwtProvider jwtProvider
    ,UserManager<ApplicationUser> userManager
    ,IAuthenticationService authenticationService,AuthenticationErrors errors) : IRequestHandler<RefreshTokenCommand, Result<AuthenticationResponse>>
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly AuthenticationErrors _errors = errors;

    public async Task<Result<AuthenticationResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.ValidateToken(request.Token);

        if (userId is null)
            return Result.Failure<AuthenticationResponse>(_errors.InvalidToken);

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure<AuthenticationResponse>(_errors.InvalidToken);

        if (user.IsDisabled)
            return Result.Failure<AuthenticationResponse>(_errors.DisabledUser);

        if (user.LockoutEnd > DateTime.UtcNow)
            return Result.Failure<AuthenticationResponse>(_errors.LockedUser);

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == request.RefreshToken && x.IsActive);

        if (userRefreshToken is null)
            return Result.Failure<AuthenticationResponse>(_errors.InvalidToken);

        userRefreshToken.RevokedOn = DateTime.UtcNow;

        var (userRoles, userPermissions) = await _authenticationService.GetUserRolesAndPermissions(user, cancellationToken);

        var (newToken, newExpiresIn) = _jwtProvider.GenerateToken(user, userRoles, userPermissions);

        var (newRefreshToken, newRefreshTokenExpiration) = _authenticationService.AddRefreshToken(user);

        await _userManager.UpdateAsync(user);

        var response = new AuthenticationResponse(user.Id, user.Email, user.FirstName, user.LastName
            , newToken, newExpiresIn, newRefreshToken, newRefreshTokenExpiration,user.UniversityId,user.FacultyId);

        return Result.Success<AuthenticationResponse>(response);

    }
}
