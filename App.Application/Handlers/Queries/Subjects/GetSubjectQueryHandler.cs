using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Contracts.Responses.Subjects;
using App.Application.Queries.Subjects;
using App.Core.Entities.Personnel;

namespace App.Application.Handlers.Queries.Subjects;

public class GetSubjectQueryHandler(IUnitOfWork unitOfWork,SubjectErrors subjectErrors) : IRequestHandler<GetSubjectQuery, Result<SubjectDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly SubjectErrors _subjectErrors = subjectErrors;


    //public async Task<Result<FacultyUserResponse>> Handle(GetFacultyUserQuery request, CancellationToken cancellationToken)
    //{
    //    var facultyUser = await _unitOfWork.FacultyUsers.FindAsync(x => x.UserId == request.Id, i => i.Include(d => d.User), cancellationToken);

    //    if (facultyUser == null)
    //        return Result.Failure<FacultyUserResponse>(_userErrors.NotFound);

    //    if (!await _facultyService.IsUserHasAccessToFaculty(_httpContextAccessor.HttpContext!.User, facultyUser.FacultyId))
    //        return Result.Failure<FacultyUserResponse>(_userErrors.Forbidden);

    //    var roles = await _userManager.GetRolesAsync(facultyUser.User);

    //    var response = facultyUser.User.Adapt<FacultyUserResponse>();

    //    response.Roles = roles.ToList();

    //    return Result.Success(response);
    //}
    public async Task<Result<SubjectDetailResponse>> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
    {
       var subject = await _unitOfWork.Subjects
            .FindAsync(x => x.Id == request.Id, i => i.Include(d => d.Prerequisites).ThenInclude(p => p.Prerequisite), cancellationToken);

        if (subject == null)
            return Result.Failure<SubjectDetailResponse>(_subjectErrors.NotFound);

        var response = new SubjectDetailResponse(
            Id: subject.Id,
            Name: subject.Name,
            Code: subject.Code,
            Description: subject.Description,
            CreditHours: subject.CreditHours,
            IsDeleted: subject.IsDeleted,
            Prerequisites: subject.Prerequisites.Select(p => new SubjectPrerequisiteResponse(
                Id: p.Prerequisite.Id,
                Name: p.Prerequisite.Name
            ))
        );

        return Result.Success<SubjectDetailResponse>(response);

    }
}