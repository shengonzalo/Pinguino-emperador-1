using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class PermissionGroupMapper : IEntityTypeConfiguration<PermissionGroup>
    {
        public void Configure(EntityTypeBuilder<PermissionGroup> builder)
        {
            builder.Ignore(e => e.Id);

            builder.ToTable("PermissionGroup");
            builder.HasKey(e => new { e.PermissionId, e.GroupId });
            builder.Property(e => e.PermissionId).HasColumnName("PermissionId");
            builder.Property(e => e.GroupId).HasColumnName("GroupId");
            builder.HasOne(e => e.Permission).WithMany(x => x.PermissionGroup).HasForeignKey(e => new { e.PermissionId });
            builder.HasOne(e => e.Group).WithMany(x => x.PermissionGroup).HasForeignKey(e => new { e.GroupId });
            builder.Configure();
        }
    }
}
