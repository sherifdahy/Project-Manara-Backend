using App.Application.Contracts.Responses.Enrollments;
using App.Application.Queries.Enrollments;
using App.Core.Entities.Relations;
using System.Linq.Expressions;

namespace App.Application.Handlers.Queries.Enrollments;

public class GetAllEnrollmentsInProgramQueryHandler(IUnitOfWork unitOfWork,ProgramErrors programErrors)
    : IRequestHandler<GetAllEnrollmentsInProgramQuery, Result<PaginatedList<ProgramEnrollmentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramErrors _programErrors = programErrors;

    public async Task<Result<PaginatedList<ProgramEnrollmentResponse>>> Handle(GetAllEnrollmentsInProgramQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Programs.GetByIdAsync(request.ProgramId) is not { } program)
            return Result.Failure<PaginatedList<ProgramEnrollmentResponse>>(_programErrors.NotFound);


        Expression<Func<StudentProgramYearTerm, bool>> query =
            x => x.ProgramId == request.ProgramId &&
                (string.IsNullOrEmpty(request.Filters.SearchValue)
                || x.User.User.Name.Contains(request.Filters.SearchValue)
                || x.YearTerm.Year.Name.Contains(request.Filters.SearchValue)
                || x.YearTerm.Term.Name.Contains(request.Filters.SearchValue)) &&
                (request.IncludeDisabled == true || x.User.User.IsDeleted == false) 
                && (!x.IsDeleted || (request.IncludeDisabled.HasValue && request.IncludeDisabled.Value));


           var count = await _unitOfWork.StudentProgramYearTerms.CountAsync(query);


        var programEnrollments = await _unitOfWork.StudentProgramYearTerms.FindAllAsync(
            query,
            i => i.Include(d => d.User.User)
                  .Include(d => d.YearTerm).ThenInclude(yt => yt.Year)
                  .Include(d => d.YearTerm).ThenInclude(yt => yt.Term),
            (request.Filters.PageNumber - 1) * request.Filters.PageSize,
            request.Filters.PageSize,
            request.Filters.SortColumn,
            request.Filters.SortDirection,
            cancellationToken);

        var response = programEnrollments.Select(e => new ProgramEnrollmentResponse(
            e.Id,
            e.YearTerm.Year.Name,
            e.YearTerm.Term.Name,
            e.User.User.Name,
            e.UserId,
            e.IsDeleted
        )).ToList();

        //var response = new ProgramEnrollmentResponse(program.Id, program.Name, enrollments);

        return Result.Success(PaginatedList<ProgramEnrollmentResponse>.Create(response, count, request.Filters.PageNumber, request.Filters.PageSize));

    }
}
