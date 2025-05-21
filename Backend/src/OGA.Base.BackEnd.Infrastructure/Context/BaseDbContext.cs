using Microsoft.EntityFrameworkCore;
using OGA.Base.BackEnd.Application.Registration;
using System.Reflection;

namespace OGA.Base.BackEnd.Infrastructure.Context;

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

        if (!string.IsNullOrEmpty(ConfigurationManager.SchemaDB))
        {
            modelBuilder.HasDefaultSchema(ConfigurationManager.SchemaDB);
        }

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
