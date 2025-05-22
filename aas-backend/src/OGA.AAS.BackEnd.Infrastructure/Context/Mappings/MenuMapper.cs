using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class MenuMapper : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu");
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.Id).HasColumnName("MenuId");
            builder.Property(e => e.Name).HasColumnName("Name");
            builder.Property(e => e.Description).HasColumnName("Description");
            builder.Property(e => e.RootId).HasColumnName("RootId");
            builder.Property(e => e.ParentId).HasColumnName("ParentId");
            builder.Property(e => e.Path).HasColumnName("Path");
            builder.Property(e => e.Order).HasColumnName("Order");
            builder.Property(e => e.ElementId).HasColumnName("ElementId");
            builder.Configure();
        }
    }
}
