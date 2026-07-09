using App.Application.Contracts.Responses.Account;
using App.Application.Queries.Account;
using App.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.Account;

public class GetStudentProfileQueryHandler(UserManager<ApplicationUser> userManager
    , IHttpContextAccessor httpContextAccessor,IUnitOfWork unitOfWork,RegistrationErrors registrationErrors) : IRequestHandler<GetStudentProfileQuery, Result<StudentProfileResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly RegistrationErrors _registrationErrors = registrationErrors;


    public async Task<Result<StudentProfileResponse>> Handle(GetStudentProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext!.User.GetUserId().ToString());

        var student = await _unitOfWork.Students.GetByIdAsync((_httpContextAccessor.HttpContext!.User.GetUserId()));

        if (student == null)
            return Result.Failure<StudentProfileResponse>(_registrationErrors.InvalidFaculty);

        var response = user.Adapt<StudentProfileResponse>();

        response.FacultyId=student.FacultyId;

        return Result.Success(response);
    }
}
