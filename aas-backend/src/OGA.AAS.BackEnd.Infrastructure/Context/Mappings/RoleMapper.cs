using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class RoleMapper : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.Id).HasColumnName("RoleId");
            builder.Property(e => e.Name).HasColumnName("Name");
            builder.Property(e => e.Description).HasColumnName("Description");
            builder.HasMany(e => e.UserRole).WithOne(x => x.Role).HasForeignKey(e => new { e.RoleId });
            builder.HasMany(e => e.RolePermissionGroup).WithOne(x => x.Role).HasForeignKey(e => new { e.RoleId });
            builder.Configure();
        }
    }
}
