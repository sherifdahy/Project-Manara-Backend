using App.Application.Contracts.Responses.StudentsPortal;
using App.Application.Errors;
using App.Application.Queries.StudentsPortal;
using App.Core.Entities.Academic;
using App.Core.Entities.Relations;
using App.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using SA.Accountring.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.StudentsPortal;

public class GetStudentLecturesQueryHandler(IUnitOfWork unitOfWork,ProgramUserErrors programUserErrors,YearErrors yearErrors) 
    : IRequestHandler<GetStudentLecturesQuery, Result<List<StudentLecturesResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramUserErrors _programUserErrors = programUserErrors;
    private readonly YearErrors _yearErrors = yearErrors;


    public async Task<Result<List<StudentLecturesResponse>>> Handle(GetStudentLecturesQuery request, CancellationToken cancellationToken)
    {
        List<StudentLecturesResponse> response = new List<StudentLecturesResponse>();

        #region Checking
        //First Get The UserId And Check That this user Is student and if not a student return error Then Get the facultyId Of this users
        var programUser = await _unitOfWork.Students
            .FindAsync(fu => fu.UserId == request.StudentId);

        if (programUser == null)
            return Result.Failure<List<StudentLecturesResponse>>(_programUserErrors.NotFound);
        #endregion


        #region studentRegistrations

        var studentRegistrations = await _unitOfWork.LectureRegistrations
            .FindAllAsync(
                x => x.StudentId == request.StudentId,
                q => q.Include(x => x.LectureSchedule).ThenInclude(x=>x.Subject),
                cancellationToken: cancellationToken);

        var prerequisites = await _unitOfWork.SubjectPrerequisites
            .FindAllAsync(
                x => x.Subject.FacultyId == programUser.FacultyId,
                q => q.Include(x => x.Subject),
                cancellationToken: cancellationToken);

        var subjectsWithPrerequisites = prerequisites
            .Select(x => x.SubjectId)
            .ToHashSet();
        #endregion


        #region GetStudentCurrentProgram And it's program
        //Get The Current Program That is assign to this student
        var studentPrograms = await _unitOfWork.StudentProgramYearTerms
        .FindAllAsync(
            x => x.UserId == request.StudentId
                && (!x.IsDeleted),
            q => q.Include(x => x.Program)
                  .Include(x => x.YearTerm).ThenInclude(yt => yt.Year)
                  .Include(x => x.YearTerm).ThenInclude(yt => yt.Term),
            cancellationToken);

        if (studentPrograms == null || studentPrograms.Count() == 0)
            return Result.Failure<List<StudentLecturesResponse>>(_programUserErrors.HasNoProgram);


        var currentStudentProgramId = studentPrograms
            .OrderByDescending(e => e.YearTerm.Year.StartDate)
            .ThenByDescending(e => e.YearTerm.TermId)
            .FirstOrDefault()?.ProgramId;
        #endregion


        #region Years And Active Term
        //Then Get the years in this faculty then search in the year term table using this years and then get the yearTerm that is Active
        var years = await _unitOfWork.AcademicYears
            .FindAllAsync(x => x.FacultyId == programUser.FacultyId, cancellationToken);

        if (years == null)
            return Result.Failure<List<StudentLecturesResponse>>(_yearErrors.NotFound);

        var activeYearTerm = await _unitOfWork.YearTerms.FindAsync(x => x.IsActive && years.Contains(x.Year));
        #endregion


        #region lectureSchendelsOfThe ActiveYearTerm
        var lectureSchendels = await _unitOfWork.LectureSchedules
            .FindAllAsync(
                x => x.ProgramId == currentStudentProgramId
                    && x.YearId == activeYearTerm!.YearId
                    && x.TermId == activeYearTerm!.TermId,
                q=>q.Include(x=>x.Subject),
                cancellationToken);
        #endregion


        #region Code And Return
        var programSubjects = await _unitOfWork.ProgramSubjects
            .FindAllAsync(
            x => x.ProgramId == currentStudentProgramId,
            q => q.Include(x => x.Subject),
            cancellationToken);

        foreach (var programSubject in programSubjects)
        {
            var registration = studentRegistrations
                .FirstOrDefault(x =>
                    x.StudentId == request.StudentId &&
                    x.LectureSchedule.SubjectId == programSubject.SubjectId);

            var lectureSchedule = lectureSchendels
                .FirstOrDefault(x => x.SubjectId == programSubject.SubjectId);

            var isSubjectCompleted = registration?.GPA > 2;

            var isSubjectCurrentlyEnrolled = registration?.GPA == 0;

            var isSubjectAvailable = lectureSchedule != null &&
                                     IsSubjectAvailable(
                                         lectureSchedule.SubjectId,
                                         studentRegistrations,
                                         subjectsWithPrerequisites,
                                         prerequisites);

            if (isSubjectCompleted)
            {
                response.Add(CreateResponse(
                    registration!.LectureScheduleId,
                    registration.LectureSchedule.Subject,
                    registration.GPA,
                    "Completed"));

                continue;
            }

            if (isSubjectCurrentlyEnrolled)
            {
                response.Add(CreateResponse(
                    registration!.LectureScheduleId,
                    registration.LectureSchedule.Subject,
                    registration.GPA,
                    "CurrentlyEnrolled"));

                continue;
            }

            if (isSubjectAvailable)
            {
                response.Add(CreateResponse(
                    lectureSchedule!.Id,
                    lectureSchedule.Subject,
                    null,
                    "Available"));

                continue;
            }

            response.Add(CreateResponse(
                null,
                programSubject.Subject,
                null,
                "Locked"));
        }

        return Result.Success(response); 
        #endregion



    }



    private static StudentLecturesResponse CreateResponse(int? lectureScheduleId,Subject subject,decimal? gpa,string status)
    {
        return new StudentLecturesResponse(
            lectureScheduleId,
            new SubjectDetailResponse(
                subject.Id,
                subject.Name,
                subject.CreditHours),
            gpa,
            status);
    }

    private static bool IsSubjectAvailable(int subjectId,IEnumerable<LectureRegistration> studentRegistrations,HashSet<int> subjectsWithPrerequisites,IEnumerable<SubjectPrerequisite> prerequisites)
    {
        return

            // Student took this subject before and failed
            studentRegistrations.Any(sr =>
                sr.LectureSchedule.SubjectId == subjectId &&
                sr.GPA > 0 &&
                sr.GPA < 2)

            ||

            // Student never took it and subject has no prerequisites
            (
                !studentRegistrations.Any(sr =>
                    sr.LectureSchedule.SubjectId == subjectId)

                &&

                !subjectsWithPrerequisites.Contains(subjectId)
            )

            ||

            // Student never took it and passed all prerequisites
            (
                !studentRegistrations.Any(sr =>
                    sr.LectureSchedule.SubjectId == subjectId)

                &&

                subjectsWithPrerequisites.Contains(subjectId)

                &&

                prerequisites
                    .Where(p => p.SubjectId == subjectId)
                    .All(p =>
                        studentRegistrations.Any(sr =>
                            sr.LectureSchedule.SubjectId == p.PrerequisiteId &&
                            sr.GPA >= 2))
            );
    }
}
