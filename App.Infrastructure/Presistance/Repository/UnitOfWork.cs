using App.Core.Entities.Academic;
using App.Core.Entities.Interfaces;
using App.Core.Entities.Personnel;
using App.Core.Entities.Relations;
using App.Core.Entities.Universities;
using Microsoft.EntityFrameworkCore.Storage;
using SA.Accountring.Core.Entities.Interfaces;

namespace App.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    // identity
    public IRepository<Scope> Scopes => new Repository<Scope>(_context);
    public IRepository<ApplicationRole> Roles =>  new Repository<ApplicationRole>(_context);
    public IRepository<IdentityRoleClaim<int>> RoleClaims => new Repository<IdentityRoleClaim<int>>(_context);
    public IRepository<ApplicationUser> Users => new Repository<ApplicationUser>(_context);
    public IRepository<IdentityUserRole<int>> UserRoles => new Repository<IdentityUserRole<int>>(_context);
    public IRepository<UserClaimOverride> UserClaimOverrides => new Repository<UserClaimOverride>(_context);
    public IRepository<RoleClaimOverride> RoleClaimOverrides => new Repository<RoleClaimOverride>(_context);
    public IRepository<FacultyUser> FacultyUsers => new Repository<FacultyUser>(_context);
    public IRepository<UniversityUser> UniversityUsers => new Repository<UniversityUser>(_context);
    public IRepository<DepartmentUser> DepartmentUsers => new Repository<DepartmentUser>(_context);
    public IRepository<ProgramUser> ProgramUsers => new Repository<ProgramUser>(_context);

    // bussiness logic 
    public IRepository<University> Universities => new Repository<University>(_context);
    public IRepository<Faculty> Fauclties => new Repository<Faculty>(_context);
    public IRepository<Department> Departments => new Repository<Department>(_context);
    public IRepository<Program> Programs => new Repository<Program>(_context);
    public IRepository<Subject> Subjects => new Repository<Subject>(_context);
    public IRepository<SubjectPrerequisite> SubjectPrerequisites => new Repository<SubjectPrerequisite>(_context);
    public IRepository<ProgramSubject> ProgramSubjects => new Repository<ProgramSubject>(_context);
    public IRepository<AcademicYear> AcademicYears => new Repository<AcademicYear>(_context);
    public IRepository<YearTerm> YearTerms => new Repository<YearTerm>(_context);
    public IRepository<Term> Terms => new Repository<Term>(_context);

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
         return await _context.Database.BeginTransactionAsync(cancellationToken);
    }
    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
