using App.Application.Commands.Departments;
using App.Application.Contracts.Responses.Departments;

namespace App.Application.Handlers.Commands.Departments;

public class CreateDepartmentCommandHandler(IUnitOfWork unitOfWork
    ,FacultyErrors facultyErrors) : IRequestHandler<CreateDepartmentCommand, Result<DepartmentResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _facultyErrors = facultyErrors;

    public async Task<Result<DepartmentResponse>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var isFacultyExists = _unitOfWork.Fauclties.IsExist(f => f.Id == request.FacultyId);

        if (!isFacultyExists)
            return Result.Failure<DepartmentResponse>(_facultyErrors.NotFound);


        var department = request.Adapt<Department>();

       await _unitOfWork.Departments.AddAsync(department);
       await _unitOfWork.SaveAsync();

       return Result.Success<DepartmentResponse>(department.Adapt<DepartmentResponse>());
    }
}
