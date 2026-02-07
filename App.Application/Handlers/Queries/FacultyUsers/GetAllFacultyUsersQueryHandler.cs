using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Contracts.Responses.Roles;
using App.Application.Queries.FacultyUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.FacultyUsers;

public class GetAllFacultyUsersQueryHandler(
    IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager,
    FacultyErrors facultyErrors
) : IRequestHandler<GetAllFacultyUsersQuery, Result<List<FacultyUserResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly FacultyErrors _facultyErrors = facultyErrors;

    public async Task<Result<List<FacultyUserResponse>>> Handle(GetAllFacultyUsersQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Fauclties.GetByIdAsync(request.FacultyId) is null)
            return Result.Failure<List<FacultyUserResponse>>(_facultyErrors.NotFound);

        var facultyUsers = await _unitOfWork.FacultyUsers.FindAllAsync(
            x => x.FacultyId == request.FacultyId && !x.User.IsDeleted,
            [i => i.User],
            cancellationToken
        );

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

        return Result.Success(response);
    }
}
