using App.Application.Commands.ProgramUsers;
using App.Core.Consts;

namespace App.Application.Handlers.Commands.ProgramUsers;

public class UpdateProgramUserCommandHandler(IUnitOfWork unitOfWork
    ,UserErrors userErrors
    ,ProgramErrors programErrors
    ,UserManager<ApplicationUser> userManager) : IRequestHandler<UpdateProgramUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly ProgramErrors _programErrors = programErrors;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result> Handle(UpdateProgramUserCommand request, CancellationToken cancellationToken)
    {
        var programUser = await _unitOfWork.ProgramUsers
            .FindAsync(x => x.UserId == request.UserId, i => i.Include(p => p.User), cancellationToken);

        if (programUser == null)
            return Result.Failure(_userErrors.NotFound);

        if (_userManager.Users.Any(x => x.Email == request.Email && x.Id != request.UserId))
            return Result.Failure(_userErrors.DuplicatedEmail);

        request.Adapt(programUser.User);

        if (request.Password != null)
        {
            programUser.User.PasswordHash = _userManager.PasswordHasher.HashPassword(programUser.User, request.Password);
        }

        var updateUserResult = await _userManager.UpdateAsync(programUser.User);

        if (updateUserResult.Succeeded)
        {
            return Result.Success();
        }

        var updateUserError = updateUserResult.Errors.First();

        return Result.Failure(new Error(updateUserError.Code, updateUserError.Description, StatusCodes.Status400BadRequest));
    }
}