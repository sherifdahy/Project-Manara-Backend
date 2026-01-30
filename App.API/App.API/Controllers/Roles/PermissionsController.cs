using App.Application.Commands.Roles;
using App.Application.Contracts.Requests.Roles;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Roles
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionsController(IMediator _mediator) : ControllerBase
    {

        //TODO 
        //UserId alwayed zero 
        [HttpPost("/api/users/{userId:int}/permissions")]
        [HasPermission(Permissions.CreatePermissions)]
        public async Task<IActionResult> AssignPermissionToUser([FromRoute]int userId,[FromBody] AssignPermissionToUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request.Adapt<AssignPermissionToUserCommand>() with { UserId = userId }, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpDelete("/api/users/{userId:int}/permissions")]
        [HasPermission(Permissions.ToggleStatusPermissions)]
        public async Task<IActionResult> TogglePermissionToUser([FromRoute] int userId, ToggleStatusPermissionRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request.Adapt<ToggleStatusPermissionCommand>() with { UserId = userId }, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
