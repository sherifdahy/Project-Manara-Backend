using App.Application.Contracts.Responses.Universities;
using App.Application.Queries.Universities;
using App.Core.Extensions;

namespace App.Application.Handlers.Queries.Universities;

public class GetMyUniversityQueryHandler
    (
        IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork,
        UniversityErrors universityErrors
    ) : IRequestHandler<GetMyUniversityQuery, Result<UniversityDetailResponse>>
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UniversityErrors _universityErrors = universityErrors;

    public async Task<Result<UniversityDetailResponse>> Handle(GetMyUniversityQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var universityUser = await _unitOfWork.UniversityUsers.FindAsync(x=>x.UserId == userId, [i=>i.University],cancellationToken);

        if (universityUser == null)
            return Result.Failure<UniversityDetailResponse>(_universityErrors.NotFoundForCurrentUser);

        var response = universityUser.University.Adapt<UniversityDetailResponse>();

        return Result.Success(response);
    }
}
