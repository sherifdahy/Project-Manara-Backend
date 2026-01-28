using App.Application.Commands.Roles;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
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
        [HttpPost("assign-permission-user")]
        [HasPermission(Permissions.CreatePermissions)]
        public async Task<IActionResult> AssignPermissionToUser([FromBody] AssignPermissionToUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpDelete("toggle-permission-status")]
        [HasPermission(Permissions.ToggleStatusPermissions)]
        public async Task<IActionResult> TogglePermissionToUser(ToggleStatusPermissionCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
