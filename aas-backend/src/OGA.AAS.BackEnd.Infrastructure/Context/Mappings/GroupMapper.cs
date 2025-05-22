using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class GroupMapper : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group");
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.Id).HasColumnName("GroupId");
            builder.Property(e => e.Name).HasColumnName("Name");
            builder.Property(e => e.Description).HasColumnName("Description");
            builder.Property(e => e.Allowed).HasColumnName("Allowed");
            builder.Property(e => e.UComments).HasColumnName("UComments");
            builder.HasMany(e => e.PermissionGroup).WithOne(x => x.Group).HasForeignKey(e => new { e.GroupId });
            builder.HasMany(e => e.RolePermissionGroup).WithOne(x => x.Group).HasForeignKey(e => new { e.GroupId });
            builder.Configure();
        }
    }
}
