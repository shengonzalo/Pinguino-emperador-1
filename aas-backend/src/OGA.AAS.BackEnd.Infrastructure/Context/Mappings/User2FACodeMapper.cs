using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class User2FACodeMapper : IEntityTypeConfiguration<User2FACode>
    {
        public void Configure(EntityTypeBuilder<User2FACode> builder)
        {
            builder.ToTable("User2FACode");
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.IdUser).HasColumnName("IdUser");
            builder.Property(e => e.Code).HasColumnName("Code");
            builder.Property(e => e.EndDate).HasColumnName("EndDate");
            builder.Configure();
        }
    }
}
