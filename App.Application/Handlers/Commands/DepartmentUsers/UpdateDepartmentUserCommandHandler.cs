using App.Application.Commands.DepartmentUsers;
using App.Application.Commands.FacultyUsers;
using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Errors;
using App.Core.Consts;
using App.Core.Entities.Personnel;
using App.Infrastructure.Repository;
using App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Commands.DepartmentUsers;

public class UpdateDepartmentUserCommandHandler(IUnitOfWork unitOfWork
    ,UserErrors userErrors
    ,IDepartmentService departmentService
    , IHttpContextAccessor httpContextAccessor
    ,UserManager<ApplicationUser> userManager
    ,RoleManager<ApplicationRole> roleManager
    ,RoleErrors roleErrors) : IRequestHandler<UpdateDepartmentUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly IDepartmentService _departmentService = departmentService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly RoleErrors _roleErrors = roleErrors;

 
    public async Task<Result> Handle(UpdateDepartmentUserCommand request, CancellationToken cancellationToken)
    {
        var departmentUser = await _unitOfWork.DepartmentUsers
            .FindAsync(x => x.UserId == request.UserId, i => i.Include(p => p.User), cancellationToken);

        if (departmentUser == null)
            return Result.Failure(_userErrors.NotFound);

        if (!await _departmentService.IsUserHasAccessToDepartment(_httpContextAccessor.HttpContext!.User, departmentUser.DepartmentId))
            return Result.Failure<FacultyUserResponse>(_userErrors.Forbidden);

        if (_userManager.Users.Any(x => x.Email == request.Email && x.Id != request.UserId))
            return Result.Failure<FacultyUserResponse>(_userErrors.DuplicatedEmail);

        foreach (var role in request.Roles)
        {

            var roleEntity = await _roleManager.FindByNameAsync(role);

            if (roleEntity is null)
                return Result.Failure<FacultyUserResponse>(_roleErrors.NotFound);

            if (roleEntity.ScopeId != DefaultScopes.Department.Id)
                return Result.Failure<FacultyUserResponse>(_roleErrors.ScopeIsNotValidForRole);
        }

        request.Adapt(departmentUser.User);

        var updateUserResult = await _userManager.UpdateAsync(departmentUser.User);

        if (updateUserResult.Succeeded)
        {
            var currentRolesAsString = await _userManager.GetRolesAsync(departmentUser.User);

            var newRolesAsString = request.Roles.Except(currentRolesAsString).ToList();

            var removedRolesAsString = currentRolesAsString.Except(request.Roles).ToList();

            if (newRolesAsString.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(departmentUser.User, newRolesAsString);
                if (!addResult.Succeeded)
                {
                    var err = addResult.Errors.First();
                    return Result.Failure(new Error(err.Code, err.Description, StatusCodes.Status400BadRequest));
                }
            }

            if (removedRolesAsString.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(departmentUser.User, removedRolesAsString);
                if (!removeResult.Succeeded)
                {
                    var err = removeResult.Errors.First();
                    return Result.Failure(new Error(err.Code, err.Description, StatusCodes.Status400BadRequest));
                }
            }

            return Result.Success();
        }

        var updateUserError = updateUserResult.Errors.First();

        return Result.Failure(new Error(updateUserError.Code, updateUserError.Description, StatusCodes.Status400BadRequest));
    }
}
