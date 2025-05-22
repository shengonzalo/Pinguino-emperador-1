using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class IdentifierTypeMapper : IEntityTypeConfiguration<IdentifierType>
    {
        public void Configure(EntityTypeBuilder<IdentifierType> builder)
        {
            builder.ToTable("IdentifierType");
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.Id).HasColumnName("IdentifierTypeId");
            builder.Property(e => e.Description).HasColumnName("Description");
            builder.Configure();
        }
    }
}
