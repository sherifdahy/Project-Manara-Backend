
using App.Infrastructure.Abstractions.Consts;
using System.Security.Cryptography;
namespace App.Application.Handlers.Commands.Authentications;

public class RegisterCommandHandler(UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider
    ,IAuthenticationService authenticationService,AuthenticationErrors errors) : IRequestHandler<RegisterCommand, Result<AuthenticationResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly AuthenticationErrors _errors = errors;

    public async Task<Result<AuthenticationResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (emailIsExists)
            return Result.Failure<AuthenticationResponse>(_errors.DuplicatedEmail);

        var user = request.Adapt<ApplicationUser>();

        user.UserName = request.Email;

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {

            await _userManager.AddToRoleAsync(user, DefaultRoles.Member);

            var (userRoles, userPermissions) = await _authenticationService.GetUserRolesAndPermissions(user, cancellationToken);

            var (token, expiresIn) = _jwtProvider.GenerateToken(user, userRoles, userPermissions); 

            var (refreshToken, refreshTokenExpiration) = _authenticationService.AddRefreshToken(user);

            await _userManager.UpdateAsync(user);

            var response = new AuthenticationResponse(user.Id, user.Email, user.FirstName, user.LastName
                , token, expiresIn, refreshToken, refreshTokenExpiration,user.UniversityId,user.FacultyId);

            return Result.Success<AuthenticationResponse>(response);
        }

        var error = result.Errors.First();

        return Result.Failure<AuthenticationResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}
