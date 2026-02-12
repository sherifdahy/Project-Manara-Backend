using App.Application.Contracts.Responses.Faculties;
using App.Application.Queries.Faculties;
using App.Core.Extensions;

namespace App.Application.Handlers.Queries.Faculties;

public record GetMyFacultyQueryHandler(FacultyErrors facultyErrors,IHttpContextAccessor httpContextAccessor,IUnitOfWork unitOfWork) : IRequestHandler<GetMyFacultyQuery, Result<FacultyDetailResponse>>
{
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor = httpContextAccessor;
    public async Task<Result<FacultyDetailResponse>> Handle(GetMyFacultyQuery request, CancellationToken cancellationToken)
    {
        var userId = _contextAccessor.HttpContext!.User.GetUserId();

        var facultyUser = await _unitOfWork.FacultyUsers.FindAsync(x=>x.UserId == userId, [i=>i.Faculty],cancellationToken);

        if (facultyUser == null)
            return Result.Failure<FacultyDetailResponse>(_facultyErrors.NotFoundForCurrentUser);

        var response = facultyUser.Faculty.Adapt<FacultyDetailResponse>();
        
        return Result.Success(response);
    }
}
