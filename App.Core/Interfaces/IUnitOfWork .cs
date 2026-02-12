using App.Core.Entities;
using App.Core.Entities.Identity;
using App.Core.Entities.Interfaces;
using App.Core.Entities.Universities;
using Microsoft.EntityFrameworkCore.Storage;

namespace SA.Accountring.Core.Entities.Interfaces;
public interface IUnitOfWork : IDisposable
{
    // identity
    public IRepository<Scope> Scopes { get; }
    public IRepository<ApplicationRole> Roles { get; }
    public IRepository<IdentityRoleClaim<int>> RoleClaims { get; }
    public IRepository<ApplicationUser> Users { get; }
    public IRepository<IdentityUserRole<int>> UserRoles { get; }
    public IRepository<UserClaimOverride> UserClaimOverrides { get; }
    public IRepository<RoleClaimOverride> RoleClaimOverrides { get; }
    public IRepository<FacultyUser> FacultyUsers { get; }
    public IRepository<UniversityUser> UniversityUsers  { get; }
    public IRepository<DepartmentUser> DepartmentUsers { get; }
    public IRepository<ProgramUser> ProgramUsers { get; }

    // bussiness logic
    public IRepository<University> Universities { get; }
    public IRepository<Faculty> Fauclties { get; }
    public IRepository<Department> Departments { get; }
    public IRepository<Program> Programs { get; }


    Task<int> SaveAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
