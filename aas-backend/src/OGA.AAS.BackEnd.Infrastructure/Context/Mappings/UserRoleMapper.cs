using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class UserRoleMapper : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(e => new { e.Id, e.RoleId });
            builder.Property(e => e.Id).HasColumnName("UserId");
            builder.Property(e => e.RoleId).HasColumnName("RoleId");
            builder.HasOne(e => e.User).WithMany(x => x.UserRole).HasForeignKey(e => new { e.Id });
            builder.HasOne(e => e.Role).WithMany(x => x.UserRole).HasForeignKey(e => new { e.RoleId });
            builder.Configure();
        }
    }
}
