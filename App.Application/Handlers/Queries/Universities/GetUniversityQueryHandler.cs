using App.Application.Contracts.Responses.Faculties;
using App.Application.Contracts.Responses.Universities;
using App.Application.Queries.Universities;
using App.Core.Consts;
using App.Core.Entities.Universities;
using App.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Application.Handlers.Queries.Universities;

public class GetUniversityQueryHandler(UniversityErrors errors
    , ApplicationDbContext context
    ,IUnitOfWork unitOfWork) : IRequestHandler<GetUniversityQuery, Result<UniversityDetailResponse>>
{
    private readonly UniversityErrors _errors = errors;
    private readonly ApplicationDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<UniversityDetailResponse>> Handle(GetUniversityQuery request, CancellationToken cancellationToken)
    {

        var university = await _context.Universities
            .Where(un => un.Id == request.Id && !un.IsDeleted)
            .Select(un => new UniversityDetailResponse(
                un.Id,
                un.Name,
                un.Description,
                un.Address,
                un.Email,
                un.Website,
                un.YearOfEstablishment,
                _context.FacultyUsers.Count(fu=>fu.Faculty.UniversityId==request.Id),
                _context.ProgramUsers.Count(pu=>pu.Program.Department.Faculty.UniversityId==request.Id),
                un.Faculties.Where(f => !f.IsDeleted).Count(),
                un.Faculties
                    .Where(f => !f.IsDeleted)
                    .Select(f => new FacultyResponse(
                        f.Id,
                        f.Name,
                        f.Description,
                        f.Address,
                        f.Email,
                        f.Website,
                        f.IsDeleted,
                        _context.FacultyUsers.Count(fu => fu.FacultyId == f.Id),
                        _context.ProgramUsers.Count(pu => pu.Program.Department.FacultyId == f.Id)
                    ))
                    .ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (university == null)
            return Result.Failure<UniversityDetailResponse>(_errors.NotFound);

        return Result.Success<UniversityDetailResponse>(university);

    }

}


