namespace App.Application.Handlers.Commands.Universities;

public class UpdateUniversityCommandHandler(IUnitOfWork unitOfWork,UniversityErrors errors): IRequestHandler<UpdateUniversityCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UniversityErrors _errors = errors;

    public async Task<Result> Handle(UpdateUniversityCommand request, CancellationToken cancellationToken)
    {
        var university = await _unitOfWork.Universities.GetByIdAsync(request.Id, cancellationToken);

        if (university == null)
            return Result.Failure(_errors.NotFound);


        if (_unitOfWork.Universities.IsExist(x => x.Name == request.Name && x.Id != request.Id))
            return Result.Failure(_errors.DuplicatedName);
            
        request.Adapt(university);

        _unitOfWork.Universities.Update(university);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
