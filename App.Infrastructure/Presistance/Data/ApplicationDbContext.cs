using App.Core.Entities.Personnel;
using App.Core.Entities.Universities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace App.Infrastructure.Presistance.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }

    #region Db Sets
    // identity
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


    #endregion


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.Entity<ApplicationUser>()
            .Property(u => u.IsDisabled)
            .HasColumnName("IsDisabled")
            .HasDefaultValue(false);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
