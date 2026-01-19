using App.Core.Entities;
using App.Core.Entities.Identity;
using App.Core.Entities.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace SA.Accountring.Core.Entities.Interfaces;
public interface IUnitOfWork : IDisposable
{
    // identity
    public IRepository<ApplicationRole> Roles { get; }
    public IRepository<IdentityRoleClaim<int>> RoleClaims { get; }
    public IRepository<ApplicationUser> Users { get; }
    public IRepository<IdentityUserRole<int>> UserRoles { get; }

    // bussiness logic
    public IRepository<University> Universities { get; }
    public IRepository<Faculty> Fauclties { get; }


    Task<int> SaveAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
