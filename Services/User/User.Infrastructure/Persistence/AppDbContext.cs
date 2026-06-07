using Microsoft.EntityFrameworkCore;

namespace User.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Domain.User> Users { get; set; }
    public DbSet<Domain.Addresses> Addresses { get; set; }
    public DbSet<Domain.Role> Roles { get; set; }
    public DbSet<Domain.RolePermission> RolePermissions { get; set; }
    public DbSet<Domain.Permission> Permissions { get; set; }
    public DbSet<Domain.RefreshToken> RefreshTokens { get; set; }

    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder optionsBuilder)
    {
        /*
         * NOTE:
         * Entity configurations can be registered manually if needed.
         * However, for better scalability, it is recommended to use
         * ApplyConfigurationsFromAssembly instead.
         *
         * Example (manual registration):
         *     modelBuilder.ApplyConfiguration(new UserConfiguration());
         */
        
        // auto-apply IEntityTypeConfiguration
        optionsBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}