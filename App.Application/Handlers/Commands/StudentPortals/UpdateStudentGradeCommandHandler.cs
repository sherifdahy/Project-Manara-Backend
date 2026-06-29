using App.Application.Commands.StudentPortals;
using App.Application.Contracts.Responses.StudentPortals;
using App.Core.Consts;
using Microsoft.AspNetCore.Identity;

namespace App.Application.Handlers.Commands.StudentPortals;

public class UpdateStudentGradeCommandHandler(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager
    ,RegistrationErrors registrationErrors) : IRequestHandler<UpdateStudentGradeCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RegistrationErrors _registrationErrors = registrationErrors;

    public async Task<Result> Handle(UpdateStudentGradeCommand request, CancellationToken cancellationToken)
    {
        
        #region Find The Registeration
        //1.0 Find By LectId and StudentId and if not then return error
        var lectureRegistration = await _unitOfWork.LectureRegistrations
            .FindAsync(x => x.StudentId == request.StudentId && x.LectureScheduleId == request.LectureScheduleId
                ,q=>q.Include(x=>x.Student),cancellationToken);

        if (lectureRegistration == null)
            return Result.Failure<RegisterLectureResponse>(_registrationErrors.lectureRegistrationNotFound);
        #endregion

        #region Check The User 
        //2.0 Get The Facuklut And Check the user by it  
        var userEntity = await _userManager.FindByIdAsync(request.UserId.ToString());
        var userRoles = await _userManager.GetRolesAsync(userEntity!);

        bool userSameFaculty = false;


        if (userRoles.Contains(RolesConstants.SystemAdmin))
            userSameFaculty = true;

        var universityUser = await _unitOfWork.UniversityUsers
            .FindAsync(fu => fu.UserId == request.UserId);

        if (universityUser != null)
        {
            userSameFaculty = await _unitOfWork.Fauclties
                .IsExistAsync(f => f.UniversityId == universityUser.UniversityId && f.Id == lectureRegistration.Student.FacultyId);
        }


        var facultyUser = await _unitOfWork.FacultyUsers
            .FindAsync(fu => fu.UserId == request.UserId);

        if (facultyUser != null)
            userSameFaculty = lectureRegistration.Student.FacultyId == facultyUser.FacultyId;


        var departmentUser = await _unitOfWork.DepartmentUsers
            .FindAsync(fu => fu.UserId == request.UserId,q=>q.Include(x=>x.Department),cancellationToken);

        if (departmentUser != null)
            userSameFaculty = lectureRegistration.Student.FacultyId == departmentUser.Department.FacultyId;

        if(!userSameFaculty)
            return Result.Failure<RegisterLectureResponse>(_registrationErrors.InvalidFaculty);

        #endregion

        #region Edit
        //3.0 Edit the GPA 

        lectureRegistration.GPA=request.GPA;

        _unitOfWork.LectureRegistrations.Update(lectureRegistration);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();


        #endregion


    }
}
