using App.API.Attributes;
using App.Application.Commands.Roles;
using App.Application.Contracts.Requests.Roles;
using App.Application.Queries.Permissions;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Roles
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionsController(IMediator _mediator) : ControllerBase
    {

        [HttpGet]
        [HasPermission(Permissions.GetPermissions)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllPermissionsQuery(), cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("/api/users/{userId:int}/permissions")]
        [RequireUserAccess("userId")]
        [HasPermission(Permissions.GetPermissions)]
        public async Task<IActionResult> GetPermissionsInUser([FromRoute] int userId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPermissionsInUserQuery(userId), cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }



        [HttpPut("/api/users/{userId:int}/permissions")]
        [RequireUserAccess("userId")]
        [HasPermission(Permissions.UpdatePermissions)]
        public async Task<IActionResult> AssignPermissionsToUser([FromRoute] int userId, [FromBody] AssignPermissionRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request.Adapt<AssignPermissionsToUserCommand>() with { UserId = userId }, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpPut("/api/roles/{roleId}/faculties/{facultyId}/permissions")]
        [RequireRoleAccess("roleId")]
        [RequireFacultyAccess("facultyId")]
        [HasPermission(Permissions.UpdatePermissions)]
        public async Task<IActionResult> AssignPermissionsToRoleFaculty([FromRoute] int roleId, [FromRoute] int facultyId, [FromBody] AssignPermissionRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request.Adapt<AssignPermissionsToRoleCommand>() with { RoleId=roleId,FacultyId=facultyId ,User=User}, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

    }
}
