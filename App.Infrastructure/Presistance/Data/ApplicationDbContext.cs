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


    // bussiness logic
    public DbSet<University> Universities { get; set; }
    public DbSet<Faculty> Faculties{ get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Student> Students { get; set; }
    //public DbSet<UserPermissionOverride> UserPermissionOverrides { get; set; }

    #endregion


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var roleEntity = builder.Entity<ApplicationRole>().Metadata;
        var roleNameIndex = roleEntity.GetIndexes()
            .FirstOrDefault(i =>
                i.Properties.Count == 1 &&
                i.Properties.First().Name == nameof(ApplicationRole.NormalizedName));

        if (roleNameIndex != null)
        {
            roleEntity.RemoveIndex(roleNameIndex);
        }

        builder.Entity<ApplicationUser>()
            .Property(u => u.IsDisabled)
            .HasColumnName("IsDisabled")
            .HasDefaultValue(false);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
