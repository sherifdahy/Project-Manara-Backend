using App.Application.Contracts.Responses.Programs;

namespace App.Application.Queries.Programs;

public record GetMyProgramQuery : IRequest<Result<ProgramDetailResponse>> { }
