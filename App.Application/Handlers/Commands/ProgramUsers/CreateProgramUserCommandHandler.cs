using App.Application.Commands.ProgramUsers;
using App.Application.Contracts.Responses.ProgramUsers;
using App.Core.Consts;
using App.Core.Entities.Personnel;
using App.Infrastructure.Abstractions.Consts;

namespace App.Application.Handlers.Commands.ProgramUsers;

public class CreateProgramUserCommandHandler(IUnitOfWork unitOfWork
    ,ProgramErrors programErrors
    ,UserManager<ApplicationUser> userManager
    ,UserErrors userErrors
    ,FacultyErrors facultyErrors) : IRequestHandler<CreateProgramUserCommand, Result<ProgramUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly UserErrors _userErrors = userErrors;
    private readonly FacultyErrors _facultyErrors = facultyErrors;


    public async Task<Result<ProgramUserResponse>> Handle(CreateProgramUserCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Fauclties.GetByIdAsync(request.FacultyId) is null)
            return Result.Failure<ProgramUserResponse>(_facultyErrors.NotFound);

        if (await _userManager.FindByEmailAsync(request.Email) is not null)
            return Result.Failure<ProgramUserResponse>(_userErrors.DuplicatedEmail);


        var applicationUser = request.Adapt<ApplicationUser>();

        applicationUser.UserName = request.Email;

        var creationResult = await _userManager.CreateAsync(applicationUser, request.Password);

        if (creationResult.Succeeded)
        {
            var roleAssignResult = await _userManager.AddToRoleAsync(applicationUser, DefaultRoles.GPAStudent);

            if (roleAssignResult.Succeeded)
            {
                var programUser = new ProgramUser()
                {
                    FacultyId = request.FacultyId,
                    UserId = applicationUser.Id,
                };

                await _unitOfWork.ProgramUsers.AddAsync(programUser, cancellationToken);
                await _unitOfWork.SaveAsync(cancellationToken);

                var response = applicationUser.Adapt<ProgramUserResponse>();

                response.Gender = programUser.User.Gender;
                response.NationalId = programUser.User.NationalId;
                response.BirthDate = programUser.User.BirthDate;

                return Result.Success(response);
            }

            var rolesAssignError = creationResult.Errors.First();

            return Result.Failure<ProgramUserResponse>(new Error(rolesAssignError.Code, rolesAssignError.Description, StatusCodes.Status400BadRequest));
        }

        var error = creationResult.Errors.First();

        return Result.Failure<ProgramUserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}

