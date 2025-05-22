using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class RolePermissionGroupMapper : IEntityTypeConfiguration<RolePermissionGroup>
    {
        public void Configure(EntityTypeBuilder<RolePermissionGroup> builder)
        {
            builder.Ignore(e => e.Id);

            builder.ToTable("RolePermissionGroup");
            builder.HasKey(e => new { e.RoleId, e.GroupId });
            builder.Property(e => e.GroupId).HasColumnName("GroupId");
            builder.Property(e => e.RoleId).HasColumnName("RoleId");
            builder.HasOne(e => e.Group).WithMany(x => x.RolePermissionGroup).HasForeignKey(e => new { e.GroupId });
            builder.HasOne(e => e.Role).WithMany(x => x.RolePermissionGroup).HasForeignKey(e => new { e.RoleId });
            builder.Configure();
        }
    }
}
