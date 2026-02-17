using App.Application.Contracts.Responses.ProgramUsers;
using App.Application.Queries.ProgramUsers;

namespace App.Application.Handlers.Queries.ProgramUsers;

public class GetProgramUserQueryHandler(IUnitOfWork unitOfWork
    ,UserErrors userErrors
    ,IProgramService programService
    ,IHttpContextAccessor httpContextAccessor
    ,UserManager<ApplicationUser> userManager) : IRequestHandler<GetProgramUserQuery, Result<ProgramUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly IProgramService _programService = programService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager = userManager;


    public async Task<Result<ProgramUserResponse>> Handle(GetProgramUserQuery request, CancellationToken cancellationToken)
    {
        var programUser = await _unitOfWork
            .ProgramUsers.FindAsync(x => x.UserId == request.Id, i => i.Include(d => d.User), cancellationToken);

        if (programUser == null)
            return Result.Failure<ProgramUserResponse>(_userErrors.NotFound);

        if (!await _programService.IsUserHasAccessToProgram(_httpContextAccessor.HttpContext!.User, programUser.ProgramId))
            return Result.Failure<ProgramUserResponse>(_userErrors.Forbidden);

        var roles = await _userManager.GetRolesAsync(programUser.User);

        var response = programUser.User.Adapt<ProgramUserResponse>();

        response.Gender = programUser.Gender;
        response.NationalId = programUser.NationalId;
        response.BirthDate = programUser.BirthDate;
        response.EnrollmentDate = programUser.EnrollmentDate;
        response.GPA = programUser.GPA;
        response.Status = programUser.Status;
        response.AcademicLevel = programUser.AcademicLevel;

        response.Roles = roles.ToList();

        return Result.Success(response);

    }
}
