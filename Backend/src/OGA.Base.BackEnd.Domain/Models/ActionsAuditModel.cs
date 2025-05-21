using OGA.Core.BackEnd.Domain.Commons;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.Domain.Models;

[ExcludeFromCodeCoverage]
public class ActionsAuditModel : BaseModel
{
    public int IdAction { get; set; }

    [SwaggerSchema(Description = "Tamaño máximo: 50")]
    public string? ActionName { get; set; }

    [SwaggerSchema(Description = "Tamaño máximo: 50")]
    public string? UserName { get; set; }

    public int IdEntity { get; set; }

    [SwaggerSchema(Description = "Tamaño máximo: 50")]
    public string? EntityName { get; set; }

    [SwaggerSchema(Description = "Tamaño máximo: 50")]
    public string? ControllerName { get; set; }

    public DateTime ActionDate { get; set; }
    public DateTime? ExpiredDate { get; set; }

    [SwaggerSchema(Description = "No maximum size")]
    public string? InfoAction { get; set; }
}
