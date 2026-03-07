using App.Application.Commands.DepartmentUsers;
using App.Application.Commands.ProgramUsers;
using App.Application.Contracts.Responses.FacultyUsers;

namespace App.Application.Handlers.Commands.ProgramUsers;

public class ToggleStatusProgramUserCommandHandler(IUnitOfWork unitOfWork
    ,UserErrors userErrors
    ,IProgramService programService
    ,IHttpContextAccessor httpContextAccessor
    ,UserManager<ApplicationUser> userManager) : IRequestHandler<ToggleStatusProgramUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly UserManager<ApplicationUser> _userManager = userManager;


    public async Task<Result> Handle(ToggleStatusProgramUserCommand request, CancellationToken cancellationToken)
    {
        var programUser = await _unitOfWork
            .ProgramUsers.FindAsync(x => x.UserId == request.Id, i => i.Include(o => o.User), cancellationToken);

        if (programUser == null)
            return Result.Failure(_userErrors.NotFound);

        programUser.User.IsDeleted = !programUser.User.IsDeleted;

        var updateResult = await _userManager.UpdateAsync(programUser.User);

        return Result.Success();
    }
}
