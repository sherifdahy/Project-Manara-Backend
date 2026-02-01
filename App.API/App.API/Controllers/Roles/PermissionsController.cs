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

        [HttpPost("/api/users/{userId:int}/permissions")]
        [HasPermission(Permissions.CreatePermissions)]
        public async Task<IActionResult> AssignPermissionToUser([FromRoute]int userId,[FromBody] AssignPermissionRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request.Adapt<AssignPermissionToUserCommand>() with { UserId = userId }, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("/api/roles/{roleId}/faculties/{facultyId}/permissions")]
        [HasPermission(Permissions.CreatePermissions)]
        public async Task<IActionResult> AssignPermissionToRoleFaculty([FromRoute] int roleId, [FromRoute] int facultyId, [FromBody] AssignPermissionRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request.Adapt<AssignPermissionToRoleCommand>() with { RoleId=roleId,FacultyId=facultyId ,User=User}, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpDelete("/api/users/{userId:int}/permissions")]
        [HasPermission(Permissions.ToggleStatusPermissions)]
        public async Task<IActionResult> TogglePermissionToUser([FromRoute] int userId, ToggleStatusPermissionRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request.Adapt<ToggleStatusPermissionCommand>() with { UserId = userId }, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpDelete("/api/roles/{roleId}/faculties/{facultyId}/permissions")]
        [HasPermission(Permissions.ToggleStatusPermissions)]
        public async Task<IActionResult> TogglePermissionToRole([FromRoute] int roleId, [FromRoute] int facultyId, ToggleStatusPermissionRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request.Adapt<ToggleStatusPermissionRoleCommand>() 
                            with { FacultyId=facultyId,RoleId=roleId,ClaimValue=request.ClaimValue }, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
