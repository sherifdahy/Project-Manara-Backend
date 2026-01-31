using App.Application.Contracts.Responses.Faculties;
using App.Application.Contracts.Responses.Universities;
using App.Application.Queries.Universities;
using App.Core.Entities.Universities;
using App.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Application.Handlers.Queries.Universities;

public class GetUniversityQueryHandler(UniversityErrors errors, ApplicationDbContext context) : IRequestHandler<GetUniversityQuery, Result<UniversityDetailResponse>>
{
    private readonly UniversityErrors _errors = errors;
    private readonly ApplicationDbContext _context = context;


    public async Task<Result<UniversityDetailResponse>> Handle(GetUniversityQuery request, CancellationToken cancellationToken)
    {
        //TODO
        //Magic string 

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
                _context.Users.Count(u => u.UniversityId == request.Id && !u.IsDisabled && _context.UserRoles
                .Any(ur => ur.UserId == u.Id && _context.Roles.Any(r => r.Id == ur.RoleId && r.Name=="Student"))),
                _context.Users.Count(u => u.UniversityId == request.Id && !u.IsDisabled && _context.UserRoles
                .Any(ur => ur.UserId == u.Id && _context.Roles.Any(r => r.Id == ur.RoleId && !(r.Name=="SystemAdmin" || r.Name=="Admin" || r.Name== "Member" || r.Name == "Student") ))),
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
                        _context.Users.Count(u => u.FacultyId == f.Id && !u.IsDisabled && _context.UserRoles
                        .Any(ur => ur.UserId == u.Id && _context.Roles.Any(r => r.Id == ur.RoleId && r.Name == "Student"))),
                        _context.Users.Count(u => u.FacultyId == f.Id && !u.IsDisabled && _context.UserRoles
                        .Any(ur => ur.UserId == u.Id && _context.Roles.Any(r => r.Id == ur.RoleId && !(r.Name == "SystemAdmin" || r.Name == "Admin" || r.Name == "Member" || r.Name == "Student"))))
                    ))
                    .ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (university == null)
            return Result.Failure<UniversityDetailResponse>(_errors.NotFound);

        return Result.Success<UniversityDetailResponse>(university);
    }

}


