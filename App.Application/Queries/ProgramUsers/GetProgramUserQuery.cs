using App.Application.Contracts.Responses.ProgramUsers;

namespace App.Application.Queries.ProgramUsers;

public record GetProgramUserQuery : IRequest<Result<ProgramUserResponse>>
{
    public int Id { get; set; }
}

