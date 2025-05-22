using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class ResourceMapper : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("Resource");
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.Id).HasColumnName("ResourceId");
            builder.Property(e => e.Name).HasColumnName("Name");
            builder.Property(e => e.Description).HasColumnName("Description");
            builder.Property(e => e.RootId).HasColumnName("RootId");
            builder.Property(e => e.ParentId).HasColumnName("ParentId");
            builder.HasOne(e => e.Root).WithMany(x => x.Descendants).HasForeignKey(e => new { e.RootId });
            builder.HasOne(e => e.Parent).WithMany(x => x.Nodes).HasForeignKey(e => new { e.ParentId });
            builder.Configure();
        }
    }
}
