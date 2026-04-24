using App.Application.Commands.Account;
using App.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Commands.Account;

public class UpdateProfileCommandHandler(IHttpContextAccessor httpContextAccessor,UserManager<ApplicationUser> userManager) : IRequestHandler<UpdateProfileCommand, Result>
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext!.User.GetUserId().ToString());

        request.Adapt(user);

        var result = await _userManager.UpdateAsync(user!);

        if(result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}
