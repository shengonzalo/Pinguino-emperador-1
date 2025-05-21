using OGA.Core.BackEnd.Domain.Commons;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.Domain.Entities;

[ExcludeFromCodeCoverage]
public class ActionsAudit : BaseEntity
{
    public int IdAction { get; set; }
    public string? ActionName { get; set; }
    public string? UserName { get; set; }
    public int IdEntity { get; set; }
    public string? EntityName { get; set; }
    public string? ControllerName { get; set; }
    public DateTime ActionDate { get; set; }
    public string? InfoAction { get; set; }
    public virtual CodeActionAudit? CodeActionAudit { get; set; }
}
