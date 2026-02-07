using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Queries.FacultyUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.FacultyUsers;

public class GetFacultyUserQueryHandler(IUnitOfWork unitOfWork,RoleManager<ApplicationRole> roleManager,UserManager<ApplicationUser> userManager) : IRequestHandler<GetFacultyUserQuery, Result<FacultyUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    
    public async Task<Result<FacultyUserResponse>> Handle(GetFacultyUserQuery request, CancellationToken cancellationToken)
    {
        var facultyUser = await _unitOfWork.FacultyUsers.FindAsync(x => x.UserId == request.Id, [i => i.User], cancellationToken);

        if (facultyUser == null)
            return Result.Failure<FacultyUserResponse>(UserErrors.NotFound);

        var roles = await _userManager.GetRolesAsync(facultyUser.User);

        var response = facultyUser.User.Adapt<FacultyUserResponse>();

        response.Roles = roles.ToList();

        return Result.Success(response);
    }
}
