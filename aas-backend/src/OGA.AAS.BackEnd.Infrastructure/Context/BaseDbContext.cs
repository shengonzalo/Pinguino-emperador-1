using Microsoft.EntityFrameworkCore;
using OGA.AAS.BackEnd.Application.Registration;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Infrastructure.Context.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context;

public class BaseDbContext : DbContext
{
    public BaseDbContext()
    {
    }

    public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            Core.BackEnd.Infrastructure.Registration.InfrastructureRegistration.RegisterDatabase(optionsBuilder);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(ConfigurationManager.SchemaDB);

        modelBuilder.ApplyConfiguration(new RoleMapper());
        modelBuilder.ApplyConfiguration(new TokenMapper());
        modelBuilder.ApplyConfiguration(new UserMapper());
        modelBuilder.ApplyConfiguration(new UserRoleMapper());
        modelBuilder.ApplyConfiguration(new MenuMapper());
        modelBuilder.ApplyConfiguration(new ResourceMapper());
        modelBuilder.ApplyConfiguration(new PermissionMapper());
        modelBuilder.ApplyConfiguration(new PermissionTypeMapper());
        modelBuilder.ApplyConfiguration(new GroupMapper());
        modelBuilder.ApplyConfiguration(new PermissionGroupMapper());
        modelBuilder.ApplyConfiguration(new RolePermissionGroupMapper());
        modelBuilder.ApplyConfiguration(new LanguageMapper());
        modelBuilder.ApplyConfiguration(new IdentifierTypeMapper());
        modelBuilder.ApplyConfiguration(new User2FACodeMapper());
    }

    public DbSet<Role>? Role { get; set; }

    public DbSet<Token>? Token { get; set; }

    public DbSet<TokenConfig>? TokenConfig { get; set; }

    public DbSet<User>? User { get; set; }

    public DbSet<UserRole>? UserRole { get; set; }

    public DbSet<Menu>? Menu { get; set; }

    public DbSet<Resource>? Resource { get; set; }

    public DbSet<Permission>? Permission { get; set; }

    public DbSet<PermissionType>? PermissionType { get; set; }

    public DbSet<Group>? Group { get; set; }

    public DbSet<PermissionGroup>? PermissionGroup { get; set; }

    public DbSet<RolePermissionGroup>? RolePermissionGroup { get; set; }

    public DbSet<Language>? Language { get; set; }

    public DbSet<IdentifierType>? IdentifierType { get; set; }

    public DbSet<User2FACode>? User2FACode { get; set; }
}
