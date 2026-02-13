using App.Application.Contracts.Responses.Universities;

namespace App.Application.Handlers.Commands.Universities;

public class CreateUniversityCommandHandler(IUnitOfWork unitOfWork,UniversityErrors errors) : IRequestHandler<CreateUniversityCommand, Result<UniversityResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UniversityErrors _errors = errors;

    public async Task<Result<UniversityResponse>> Handle(CreateUniversityCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Universities.IsExistAsync(x => x.Name == request.Name))
            return Result.Failure<UniversityResponse>(_errors.DuplicatedName);

        var university = request.Adapt<University>();

        await _unitOfWork.Universities.AddAsync(university, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(university.Adapt<UniversityResponse>());
    }
}
