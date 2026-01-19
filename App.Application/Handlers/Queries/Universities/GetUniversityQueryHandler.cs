using App.Application.Queries.Universities;
using App.Application.Responses.Faculties;
using System.Linq.Expressions;

namespace App.Application.Handlers.Queries.Universities;

public class GetUniversityQueryHandler(IUnitOfWork unitOfWork, UniversityErrors errors) : IRequestHandler<GetUniversityQuery, Result<UniversityDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UniversityErrors _errors = errors;

    #region OldCode
    //public async Task<Result<UniversityDetailResponse>> Handle(GetUniversityQuery request, CancellationToken cancellationToken)
    //{
    //    var university = await _unitOfWork.Universities.FindAsync(x => x.Id == request.Id,
    //                                                              new Expression<Func<University, object>>[] { u => u.Faculties.Where(x => x.IsDeleted == false) },
    //                                                              cancellationToken);

    //    if (university == null)
    //        return Result.Failure<UniversityDetailResponse>(_errors.NotFound);

    //    var response = university.Adapt<UniversityDetailResponse>();

    //    return Result.Success(response);
    //} 
    #endregion

    public async Task<Result<UniversityDetailResponse>> Handle( GetUniversityQuery request,CancellationToken cancellationToken)
    {
        var university = await _unitOfWork.Universities
            .Query()
            .Where(u => u.Id == request.Id)
            .Select(u => new UniversityDetailResponse(
                u.Id,
                u.Name,
                u.Description,
                u.Address,
                u.Email,
                u.Website,
                u.Faculties
                    .Where(f => !f.IsDeleted)
                    .Select(f => new FacultyResponse(
                        f.Id,
                        f.Name,
                        f.Description,
                        f.Address,
                        f.Email,
                        f.Website,
                        f.IsDeleted,
                        f.Departments
                            .SelectMany(d => d.Programs)
                            .SelectMany(p => p.Students)
                            .Count()
                    ))
                    .ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (university == null)
            return Result.Failure<UniversityDetailResponse>(_errors.NotFound);

        return Result.Success(university);
    }

}
