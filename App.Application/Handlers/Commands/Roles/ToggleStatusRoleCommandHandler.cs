using App.Application.Abstractions;
using App.Application.Commands.Roles;
using App.Application.Errors;
using App.Core.Entities.Identity;
using App.Infrastructure.Abstractions.Consts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Commands.Roles;

public class ToggleStatusRoleCommandHandler(RoleManager<ApplicationRole> roleManager,RoleErrors errors) : IRequestHandler<ToggleStatusRoleCommand, Result>
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly RoleErrors _errors = errors;

    public async Task<Result> Handle(ToggleStatusRoleCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == DefaultRoles.SystemAdminRoleId)
            return Result.Failure(_errors.ModificationForbidden);

        var role = await _roleManager.FindByIdAsync(request.Id.ToString());

        if (role is null)
            return Result.Failure(_errors.NotFound);

        role.IsDeleted = !role.IsDeleted;

        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description,StatusCodes.Status400BadRequest));

    }
}
