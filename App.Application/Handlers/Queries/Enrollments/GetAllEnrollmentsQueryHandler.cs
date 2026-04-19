using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Enrollments;
using App.Application.Queries.Departments;
using App.Application.Queries.Enrollments;
using App.Core.Entities.Personnel;

namespace App.Application.Handlers.Queries.Enrollments;

public class GetAllEnrollmentsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllEnrollmentsQuery, Result<List<EnrollmentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<EnrollmentResponse>>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await _unitOfWork.StudentProgramYearTerms
            .FindAllAsync(
                x => x.UserId == request.UserId
                    && (!x.IsDeleted || (request.IncludeDisabled.HasValue && request.IncludeDisabled.Value)),
                q => q.Include(x => x.Program)
                      .Include(x => x.YearTerm).ThenInclude(yt => yt.Year)
                      .Include(x => x.YearTerm).ThenInclude(yt => yt.Term),
                cancellationToken);

        var currentEnrollmentId = enrollments
            .OrderByDescending(e => e.YearTerm.Year.StartDate)
            .ThenByDescending(e => e.YearTerm.TermId)
            .FirstOrDefault()?.Id;

        var response = enrollments.Select(e => new EnrollmentResponse(
                    e.Id,
                    e.Program.Name,
                    e.YearTerm.Year.Name,
                    e.YearTerm.Term.Name,
                    e.UserId,
                    e.Id == currentEnrollmentId 
                )).ToList();

        return Result.Success(response);
    }
}
