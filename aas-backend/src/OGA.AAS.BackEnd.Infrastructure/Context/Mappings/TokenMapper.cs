using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class TokenMapper : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.ToTable("Token");
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.Id).HasColumnName("TokenId");
            builder.Property(e => e.AccessToken).HasColumnName("AccessToken");
            builder.Property(e => e.Email).HasColumnName("Email");
            builder.Property(e => e.ExpiredDate).HasColumnName("ExpiredDate");
            builder.Configure();
        }
    }
}
