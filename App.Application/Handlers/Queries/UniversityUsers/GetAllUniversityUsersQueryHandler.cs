using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Errors;
using App.Application.Queries.FacultyUsers;
using App.Application.Queries.UniversityUsers;
using App.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.UniversityUsers;

public class GetAllUniversityUsersQueryHandler(IUnitOfWork unitOfWork
    ,UniversityErrors universityErrors,UserManager<ApplicationUser> userManager) : IRequestHandler<GetAllUniversityUsersQuery, Result<List<FacultyUserResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UniversityErrors _universityErrors = universityErrors;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<List<FacultyUserResponse>>> Handle(GetAllUniversityUsersQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Universities.GetByIdAsync(request.UniversityId) is null)
            return Result.Failure<List<FacultyUserResponse>>(_universityErrors.NotFound);

        var universityUsers = await _unitOfWork.UniversityUsers.FindAllAsync(
            x => x.UniversityId == request.UniversityId && !x.User.IsDeleted,
            i => i.Include(o=>o.User),
            cancellationToken
        );

        var response = new List<FacultyUserResponse>();

        foreach (var x in universityUsers)
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

        return Result.Success(response);
    }
}
