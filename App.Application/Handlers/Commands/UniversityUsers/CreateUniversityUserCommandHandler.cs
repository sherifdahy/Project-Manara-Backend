using App.Application.Commands.FacultyUsers;
using App.Application.Commands.UniversityUsers;
using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Errors;
using App.Core.Consts;
using App.Core.Entities.Personnel;
using App.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Commands.UniversityUsers;

public class CreateUniversityUserCommandHandler(UserManager<ApplicationUser> userManager
    ,RoleErrors roleErrors
    ,IUnitOfWork unitOfWork
    ,UserErrors userErrors
    ,RoleManager<ApplicationRole> roleManager
    ,UniversityErrors universityErrors) : IRequestHandler<CreateUniversityUserCommand, Result<FacultyUserResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleErrors _roleErrors = roleErrors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly UniversityErrors _universityErrors = universityErrors;

    public async Task<Result<FacultyUserResponse>> Handle(CreateUniversityUserCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Universities.GetByIdAsync(request.UniversityId) is null)
            return Result.Failure<FacultyUserResponse>(_universityErrors.NotFound);

        if (await _userManager.FindByEmailAsync(request.Email) is not null)
            return Result.Failure<FacultyUserResponse>(_userErrors.DuplicatedEmail);

        foreach (var role in request.Roles)
        {
            var roleEntity = await _roleManager.FindByNameAsync(role);

            if (roleEntity is null)
                return Result.Failure<FacultyUserResponse>(_roleErrors.NotFound);

            if (roleEntity.ScopeId != DefaultScopes.University.Id)
                return Result.Failure<FacultyUserResponse>(_roleErrors.ScopeIsNotValidForRole);
        }

        var applicationUser = request.Adapt<ApplicationUser>();

        applicationUser.UserName = request.Email;

        var creationResult = await _userManager.CreateAsync(applicationUser, request.Password);

        if (creationResult.Succeeded)
        {
            var roleAssignResult = await _userManager.AddToRolesAsync(applicationUser, request.Roles);

            if (roleAssignResult.Succeeded)
            {
                var universityUser = new UniversityUser()
                {
                    UniversityId = request.UniversityId,
                    UserId = applicationUser.Id,
                };

                await _unitOfWork.UniversityUsers.AddAsync(universityUser, cancellationToken);
                await _unitOfWork.SaveAsync(cancellationToken);

                var response = applicationUser.Adapt<FacultyUserResponse>();

                response.Roles = request.Roles;

                return Result.Success(response);
            }

            var rolesAssignError = creationResult.Errors.First();

            return Result.Failure<FacultyUserResponse>(new Error(rolesAssignError.Code, rolesAssignError.Description, StatusCodes.Status400BadRequest));
        }

        var error = creationResult.Errors.First();

        return Result.Failure<FacultyUserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }


}
