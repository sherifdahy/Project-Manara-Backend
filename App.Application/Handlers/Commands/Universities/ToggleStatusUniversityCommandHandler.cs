namespace App.Application.Handlers.Commands.Universities;

public class ToggleStatusUniversityCommandHandler(IUnitOfWork unitOfWork, UniversityErrors errors) : IRequestHandler<ToggleStatusUniveristyCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UniversityErrors _errors = errors;

    public async Task<Result> Handle(ToggleStatusUniveristyCommand request, CancellationToken cancellationToken)
    {
        var university = await _unitOfWork.Universities.GetByIdAsync(request.Id);

        if (university is null)
            return Result.Failure(_errors.NotFound);

        university.IsDeleted = !university.IsDeleted;

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
