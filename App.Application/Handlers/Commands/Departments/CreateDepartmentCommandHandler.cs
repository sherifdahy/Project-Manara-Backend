using App.Application.Commands.Departments;
using App.Application.Contracts.Responses.Departments;

namespace App.Application.Handlers.Commands.Departments;

public class CreateDepartmentCommandHandler(IUnitOfWork unitOfWork
    ,FacultyErrors facultyErrors
    ,DepartmentErrors departmentErrors) : IRequestHandler<CreateDepartmentCommand, Result<DepartmentResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly DepartmentErrors _departmentErrors = departmentErrors;

    public async Task<Result<DepartmentResponse>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var isFacultyExists = await _unitOfWork.Fauclties.IsExistAsync(f => f.Id == request.FacultyId);

        if (!isFacultyExists)
            return Result.Failure<DepartmentResponse>(_facultyErrors.NotFound);

        var isDepartmentExists = await  _unitOfWork.Departments
            .IsExistAsync(x=>x.FacultyId==request.FacultyId && x.Name==request.Name);

        if (isDepartmentExists)  
            return Result.Failure<DepartmentResponse>(_departmentErrors.DuplicatedName);

        var department = request.Adapt<Department>();

       await _unitOfWork.Departments.AddAsync(department);
       await _unitOfWork.SaveAsync();

       return Result.Success<DepartmentResponse>(department.Adapt<DepartmentResponse>());
    }
}
