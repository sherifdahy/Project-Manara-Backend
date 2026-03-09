

using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Contracts.Responses.Subjects;
using App.Application.Queries.Subjects;
using System.Linq.Expressions;

namespace App.Application.Handlers.Queries.Subjects;

public class GetAllSubjectQueryHandler(IUnitOfWork unitOfWork
    ,FacultyErrors facultyErrors) : IRequestHandler<GetAllSubjectsQuery, Result<PaginatedList<SubjectResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _facultyErrors = facultyErrors;


    public async Task<Result<PaginatedList<SubjectResponse>>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Fauclties.GetByIdAsync(request.FacultyId) is null)
            return Result.Failure<PaginatedList<SubjectResponse>>(_facultyErrors.NotFound);

        Expression<Func<Subject, bool>> query =
            x => x.FacultyId == request.FacultyId &&
                (string.IsNullOrEmpty(request.Filters.SearchValue) || x.Name.Contains(request.Filters.SearchValue) || x.Code!.Contains(request.Filters.SearchValue) || x.Description.Contains(request.Filters.SearchValue)) &&
                (request.IncludeDisabled == true || x.IsDeleted == false);

        var count = await _unitOfWork.Subjects.CountAsync(query);

        var subjects = await _unitOfWork.Subjects.FindAllAsync(
            query,
            null,
            (request.Filters.PageNumber - 1) * request.Filters.PageSize,
            request.Filters.PageSize,
            request.Filters.SortColumn,
            request.Filters.SortDirection,
            cancellationToken);


        var response = subjects.Adapt<List<SubjectResponse>>();

       return Result.Success(PaginatedList<SubjectResponse>.Create(response, count, request.Filters.PageNumber, request.Filters.PageSize));

    }
}

