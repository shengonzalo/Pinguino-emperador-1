using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.Base.BackEnd.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.Infrastructure.Context.Mappings;

[ExcludeFromCodeCoverage]
public class ActionsAuditMapper : IEntityTypeConfiguration<ActionsAudit>
{
    public void Configure(EntityTypeBuilder<ActionsAudit> builder)
    {
        builder.ToTable("ActionsAudit");
        builder.HasKey(d => d.Id);

        builder.HasOne(a => a.CodeActionAudit).WithMany().HasForeignKey(x => x.IdAction);
    }

}
