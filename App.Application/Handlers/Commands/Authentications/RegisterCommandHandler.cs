
using App.Application.Contracts.Responses.Authentications;
using App.Infrastructure.Abstractions.Consts;
using System.Security.Cryptography;
namespace App.Application.Handlers.Commands.Authentications;

public class RegisterCommandHandler(UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider
    ,IAuthenticationService authenticationService
    ,AuthenticationErrors errors
    ,IUnitOfWork unitOfWork
    ,UniversityErrors universityErrors
    ,FacultyErrors facultyErrors) : IRequestHandler<RegisterCommand, Result<AuthenticationResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly AuthenticationErrors _errors = errors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UniversityErrors _universityErrors = universityErrors;
    private readonly FacultyErrors _facultyErrors = facultyErrors;

    public async Task<Result<AuthenticationResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (emailIsExists)
            return Result.Failure<AuthenticationResponse>(_errors.DuplicatedEmail);

        //if (!(_unitOfWork.Universities.IsExist(x => x.Id == request.UniversityId)))
        //    return Result.Failure<AuthenticationResponse>(_universityErrors.InvalidId);

        //if (!(_unitOfWork.Fauclties.IsExist(x => (x.Id == request.FacultyId) &&(x.UniversityId==request.UniversityId )) || request.FacultyId is null))
        //    return Result.Failure<AuthenticationResponse>(_facultyErrors.InvalidId);

        var user = request.Adapt<ApplicationUser>();

        user.UserName = request.Email;

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {

            await _userManager.AddToRoleAsync(user, DefaultRoles.Member);

            var (userRoles, userPermissions) = await _authenticationService.GetUserOverrideRolesAndPermissions(user, cancellationToken);

            var (token, expiresIn) = _jwtProvider.GenerateToken(user, userRoles, userPermissions); 

            var (refreshToken, refreshTokenExpiration) = _authenticationService.AddRefreshToken(user);

            await _userManager.UpdateAsync(user);

            var response = new AuthenticationResponse(user.Id, user.Email, user.Name
                , token, expiresIn, refreshToken, refreshTokenExpiration);

            return Result.Success<AuthenticationResponse>(response);
        }

        var error = result.Errors.First();

        return Result.Failure<AuthenticationResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}
