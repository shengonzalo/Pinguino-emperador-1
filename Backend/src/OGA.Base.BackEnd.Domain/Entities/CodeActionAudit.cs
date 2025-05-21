using OGA.Core.BackEnd.Domain.Commons;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.Domain.Entities;

[ExcludeFromCodeCoverage]
public class CodeActionAudit : BaseEntity
{
    public string? Description { get; set; }
}
