using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Contracts.Responses.Roles;
using App.Application.Queries.FacultyUsers;
using App.Core.Entities.Personnel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace App.Application.Handlers.Queries.FacultyUsers;

public class GetAllFacultyUsersQueryHandler(
    IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager,
    FacultyErrors facultyErrors
) : IRequestHandler<GetAllFacultyUsersQuery, Result<PaginatedList<FacultyUserResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly FacultyErrors _facultyErrors = facultyErrors;

    public async Task<Result<PaginatedList<FacultyUserResponse>>> Handle(GetAllFacultyUsersQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Fauclties.GetByIdAsync(request.FacultyId) is null)
            return Result.Failure<PaginatedList<FacultyUserResponse>>(_facultyErrors.NotFound);

        Expression<Func<FacultyUser, bool>> query = 
            x => x.FacultyId == request.FacultyId && 
                (string.IsNullOrEmpty(request.Filters.SearchValue) || x.User.Name.Contains(request.Filters.SearchValue) || x.User.Email!.Contains(request.Filters.SearchValue) || x.User.SSN.Contains(request.Filters.SearchValue)) && 
                (request.IncludeDisabled == true || x.User.IsDeleted == false);

        var count = await _unitOfWork.FacultyUsers.CountAsync(query);

        var facultyUsers = await _unitOfWork.FacultyUsers.FindAllAsync(
            query,
            request.Filters.PageSize,
            (request.Filters.PageNumber - 1) * request.Filters.PageSize,
            //request.Filters.SortColumn,
            //request.Filters.SortDirection,
            cancellationToken);

        var response = new List<FacultyUserResponse>();

        foreach (var x in facultyUsers)
        {
            var roles = (await _userManager.GetRolesAsync(x.User)).ToList();

            response.Add(new FacultyUserResponse
            {
                Id = x.UserId,
                Email = x.User.Email!,
                Name = x.User.Name,
                SSN = x.User.SSN,
                Roles = roles
            });
        }

        return Result.Success(PaginatedList<FacultyUserResponse>.Create(response,count,request.Filters.PageNumber,request.Filters.PageSize));
    }
}
