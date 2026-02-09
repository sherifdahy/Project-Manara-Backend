using App.Application.Abstractions;
using App.Application.Contracts.Responses.Roles;
using MediatR;

namespace App.Application.Queries.Roles;

public record GetAllRolesQuery(bool? IncludeDisabled = false) : IRequest<Result<List<RoleResponse>>>;