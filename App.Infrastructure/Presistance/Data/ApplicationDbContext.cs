using App.Core.Entities.Personnel;
using App.Core.Entities.Universities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace App.Infrastructure.Presistance.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }

    #region Db Sets
    // identity
    public DbSet<Scope> Scopes { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserClaimOverride> UserClaimOverrides { get; set; }
    public DbSet<RoleClaimOverride> RoleClaimOverrides { get; set; }
    public DbSet<FacultyUser> FacultyUsers { get; set; }
    public DbSet<UniversityUser> UniversityUsers { get; set; }
    public DbSet<DepartmentUser> DepartmentUsers { get; set; }
    public DbSet<ProgramUser> ProgramUsers { get; set; }


    // bussiness logic
    public DbSet<University> Universities { get; set; }
    public DbSet<Faculty> Faculties{ get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<SubjectPrerequisite> SubjectPrerequisites { get; set; }

    #endregion

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //var entries = ChangeTracker.Entries<AuditableEntity>();

        //if (entries.Any())
        //{
        //    var currentUserId = _httpContext.HttpContext!.User.GetUserId();
        //    foreach (var entityEntry in entries)
        //    {
        //        if (entityEntry.State == EntityState.Added)
        //        {
        //            entityEntry.Property(x => x.CreatedById).CurrentValue = currentUserId;
        //            entityEntry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
        //        }
        //        else if (entityEntry.State == EntityState.Modified)
        //        {
        //            entityEntry.Property(x => x.UpdatedById).CurrentValue = currentUserId;
        //            entityEntry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;

        //        }
        //    }
        //}

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
            .ToList()
            .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);

        builder.Entity<ApplicationUser>()
            .Property(u => u.IsDisabled)
            .HasColumnName("IsDisabled")
            .HasDefaultValue(false);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
