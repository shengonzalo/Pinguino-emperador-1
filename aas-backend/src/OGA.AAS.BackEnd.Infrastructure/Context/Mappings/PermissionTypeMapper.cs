using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class PermissionTypeMapper : IEntityTypeConfiguration<PermissionType>
    {
        public void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            builder.ToTable("PermissionType");
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.Id).HasColumnName("PermissionTypeId");
            builder.Property(e => e.Description).HasColumnName("Description");
            builder.Configure();
        }
    }
}
