using App.Application.Commands.Subjects;
using App.Application.Contracts.Responses.Subjects;
using App.Application.Errors;
using App.Infrastructure.Abstractions.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Data;
using System.Security.Claims;

namespace App.Application.Handlers.Commands.Subjects;

public class UpdateSubjectCommandHandler(IUnitOfWork unitOfWork
    ,SubjectErrors subjectErrors) : IRequestHandler<UpdateSubjectCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly SubjectErrors _subjectErrors = subjectErrors;

    public async Task<Result> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {        

        var subject = await _unitOfWork.Subjects
            .FindAsync(
                criteria: x => x.Id == request.Id,
                includes: q => q.Include(s => s.Prerequisites)
            );

        if (subject is null)
            return Result.Failure(_subjectErrors.NotFound);


        if (await _unitOfWork.Subjects.IsExistAsync(x => x.FacultyId == subject.FacultyId && x.Name == request.Name && x.Id != request.Id))
            return Result.Failure(_subjectErrors.DuplicatedName);


        foreach(var requestPrerequisiteId in request.PrerequisiteIds)
        {
            if (requestPrerequisiteId == subject.Id)
                return Result.Failure(_subjectErrors.SubjectDependItSelf);
        }

        var currentPrerequisites = subject.Prerequisites.ToList();

        var newPrerequisiteIds = request.PrerequisiteIds.Except(currentPrerequisites.Select(x => x.PrerequisiteId));


        foreach (var newPrerequisiteId in newPrerequisiteIds)
        {
            var isPrerequisiteExists = await _unitOfWork.Subjects
                .IsExistAsync(x => x.FacultyId == subject.FacultyId && x.Id == newPrerequisiteId);

            if (!isPrerequisiteExists)
                return Result.Failure(_subjectErrors.NotFound);

            subject.Prerequisites.Add(new SubjectPrerequisite
            {
                PrerequisiteId = newPrerequisiteId
            });
        }


        var removedPrerequisiteIds = currentPrerequisites.Select(x => x.PrerequisiteId).Except(request.PrerequisiteIds);

        foreach(var removedPrerequisiteId in removedPrerequisiteIds)
        {
            subject.Prerequisites.Remove(currentPrerequisites.First(p=> p.SubjectId==request.Id && p.PrerequisiteId==removedPrerequisiteId));
        }

        subject.Name= request.Name;
        subject.Description= request.Description;
        subject.Code= request.Code;
        subject.CreditHours= request.CreditHours;

        _unitOfWork.Subjects.Update(subject);
        await _unitOfWork.SaveAsync();

        return Result.Success();


    }
}
