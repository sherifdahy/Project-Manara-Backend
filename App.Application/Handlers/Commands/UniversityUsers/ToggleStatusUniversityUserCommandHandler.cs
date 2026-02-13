using App.Application.Commands.FacultyUsers;
using App.Application.Commands.UniversityUsers;
using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Errors;
using App.Core.Entities.Personnel;
using App.Infrastructure.Repository;
using App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Commands.UniversityUsers;

public class ToggleStatusUniversityUserCommandHandler(IUnitOfWork unitOfWork
    ,UserErrors userErrors
    ,IUniversityService universityService
    , IHttpContextAccessor httpContextAccessor
    ,UserManager<ApplicationUser> userManager) : IRequestHandler<ToggleStatusUniversityUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly IUniversityService _universityService = universityService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result> Handle(ToggleStatusUniversityUserCommand request, CancellationToken cancellationToken)
    {
        var universityUser = await _unitOfWork.UniversityUsers
            .FindAsync(x => x.UserId == request.Id, i => i.Include(p=>p.User), cancellationToken);

        if (universityUser == null)
            return Result.Failure(_userErrors.NotFound);

        if (!await _universityService.IsUserHasAccessToUniversity(_httpContextAccessor.HttpContext!.User, universityUser.UniversityId))
            return Result.Failure<FacultyUserResponse>(_userErrors.Forbidden);

        universityUser.User.IsDeleted = !universityUser.User.IsDeleted;

        var updateResult = await _userManager.UpdateAsync(universityUser.User);

        return Result.Success();

    }
}
