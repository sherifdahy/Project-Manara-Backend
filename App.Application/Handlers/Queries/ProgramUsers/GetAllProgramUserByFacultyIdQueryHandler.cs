using App.Application.Contracts.Responses.ProgramUsers;
using App.Application.Queries.ProgramUsers;
using App.Core.Entities.Personnel;
using App.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace App.Application.Handlers.Queries.ProgramUsers;

public class GetAllProgramUserByFacultyIdQueryHandler(
    IUnitOfWork unitOfWork,
    FacultyErrors facultyErrors,
    UserManager<ApplicationUser> userManager) : IRequestHandler<GetAllProgramUserByFacultyIdQuery, Result<PaginatedList<ProgramUserResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<Result<PaginatedList<ProgramUserResponse>>> Handle(GetAllProgramUserByFacultyIdQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Fauclties.GetByIdAsync(request.FacultyId) is null)
            return Result.Failure<PaginatedList<ProgramUserResponse>>(_facultyErrors.NotFound);

        Expression<Func<ProgramUser, bool>> query =
            x => x.FacultyId == request.FacultyId &&
                (string.IsNullOrEmpty(request.Filters.SearchValue)
                || x.User.Name.Contains(request.Filters.SearchValue)
                || x.User.Email!.Contains(request.Filters.SearchValue)
                || x.User.NationalId.Contains(request.Filters.SearchValue)) &&
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

            var temp = x.User.Adapt<ProgramUserResponse>();

            temp.Roles = roles;

            response.Add(temp);
        }

        return Result.Success(PaginatedList<ProgramUserResponse>.Create(response, count, request.Filters.PageNumber, request.Filters.PageSize));
    }
}
