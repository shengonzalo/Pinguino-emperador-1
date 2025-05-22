using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class UserKeyMapper : IEntityTypeConfiguration<UserKey>
    {
        public void Configure(EntityTypeBuilder<UserKey> builder)
        {
            builder.ToTable("UserKey");
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.IdUser).HasColumnName("IdUser");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate");
            builder.Property(e => e.PasswordHash).HasColumnName("PasswordHash");
            builder.HasOne(l => l.User).WithMany(u=>u.UserKeys).HasForeignKey(k => k.IdUser);
            builder.Configure();
        }
    }
}
