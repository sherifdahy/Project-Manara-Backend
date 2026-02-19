using App.Application.Contracts.Responses.ProgramUsers;
using App.Application.Queries.ProgramUsers;
using App.Core.Entities.Personnel;
using System.Linq.Expressions;

namespace App.Application.Handlers.Queries.ProgramUsers;

public class GetAllProgramUsersQueryHandler(IUnitOfWork unitOfWork
    ,ProgramErrors programErrors
    ,UserManager<ApplicationUser> userManager) : IRequestHandler<GetAllProgramUsersQuery, Result<PaginatedList<ProgramUserResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramErrors _programErrors = programErrors;
    private readonly UserManager<ApplicationUser> _userManager = userManager;


    public async Task<Result<PaginatedList<ProgramUserResponse>>> Handle(GetAllProgramUsersQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Programs.GetByIdAsync(request.ProgramId) is null)
            return Result.Failure<PaginatedList<ProgramUserResponse>>(_programErrors.NotFound);

        Expression<Func<ProgramUser, bool>> query =
            x => x.ProgramId == request.ProgramId &&
                (string.IsNullOrEmpty(request.Filters.SearchValue)
                || x.User.Name.Contains(request.Filters.SearchValue)
                || x.User.Email!.Contains(request.Filters.SearchValue)
                || x.User.SSN.Contains(request.Filters.SearchValue)) &&
                (request.IncludeDisabled == true || x.User.IsDeleted == false);

        var count = await _unitOfWork.ProgramUsers.CountAsync(query);

        var programUsers = await _unitOfWork.ProgramUsers.FindAllAsync(
            query,
            i => i.Include(d => d.User),
            (request.Filters.PageNumber - 1) * request.Filters.PageSize,
            request.Filters.PageSize,
            request.Filters.SortColumn,
            request.Filters.SortDirection,
            cancellationToken);

        var response = new List<ProgramUserResponse>();

        foreach (var x in programUsers)
        {
            var roles = (await _userManager.GetRolesAsync(x.User)).ToList();

            response.Add(new ProgramUserResponse
            {
                Id = x.UserId,
                Email = x.User.Email!,
                Name = x.User.Name,
                SSN = x.User.SSN,
                Roles = roles,
                IsDisabled = x.User.IsDisabled,
                Gender=x.Gender,
                NationalId=x.NationalId,
                BirthDate=x.BirthDate,
                EnrollmentDate=x.EnrollmentDate,
                GPA=x. GPA,
                Status=x.Status,
                AcademicLevel=x.AcademicLevel

            });

        }

        return Result.Success(PaginatedList<ProgramUserResponse>.Create(response, count, request.Filters.PageNumber, request.Filters.PageSize));


    }
}


