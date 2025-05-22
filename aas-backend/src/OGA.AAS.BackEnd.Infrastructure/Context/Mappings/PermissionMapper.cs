using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class PermissionMapper : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission");
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.Id).HasColumnName("PermissionId");
            builder.Property(e => e.Name).HasColumnName("Name");
            builder.Property(e => e.Description).HasColumnName("Description");
            builder.Property(e => e.Allowed).HasColumnName("Allowed");
            builder.Property(e => e.PermissionTypeId).HasColumnName("PermissionTypeId");
            builder.Property(e => e.ResourceId).HasColumnName("ResourceId");
            builder.HasMany(e => e.PermissionGroup).WithOne(x => x.Permission).HasForeignKey(e => new { e.PermissionId });
            builder.HasOne(e => e.PermissionType).WithMany().HasForeignKey(e => new { e.PermissionTypeId });
            builder.HasOne(e => e.Resource).WithMany(x => x.Permission).HasForeignKey(e => new { e.ResourceId });
            builder.Configure();
        }
    }
}
