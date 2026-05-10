using App.Application.Contracts.Responses.Programs;
using App.Application.Queries.Programs;
using App.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.Programs
{
    internal class GetSectionScheduleQueryHandler(IUnitOfWork unitOfWork,ProgramErrors programErrors,YearErrors yearErrors) : IRequestHandler<GetSectionSchedulesQuery, Result<List<SectionScheduleItemResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ProgramErrors _programErrors = programErrors;
        private readonly YearErrors _yearErrors = yearErrors;
        public async Task<Result<List<SectionScheduleItemResponse>>> Handle(GetSectionSchedulesQuery request, CancellationToken cancellationToken)
        {
            #region Get Active Year Term
            var currentFaculty = await _unitOfWork.Fauclties.FindAsync(x=>x.Departments.Any(x=>x.Programs.Any(x=>x.Id == request.ProgramId)));

            var yearTerm = await _unitOfWork.YearTerms.FindAsync(x => x.Year.FacultyId == currentFaculty!.Id && x.IsActive);

            if (yearTerm == null)
                return Result.Failure<List<SectionScheduleItemResponse>>(_yearErrors.NoActiveYearTerm);
            #endregion

            var existingSchedules = await _unitOfWork.SectionSchedules.FindAllAsync(x => x.ProgramId == request.ProgramId && (x.YearId == yearTerm.YearId && x.TermId == yearTerm.TermId) && x.Subject.IsDeleted != true && x.Period.IsDeleted != true, x => x.Include(d => d.Subject).Include(x=>x.Instructor).ThenInclude(i=>i.User), cancellationToken);

            var response = existingSchedules.Adapt<List<SectionScheduleItemResponse>>();

            return Result.Success(response);
        }
    }
}
