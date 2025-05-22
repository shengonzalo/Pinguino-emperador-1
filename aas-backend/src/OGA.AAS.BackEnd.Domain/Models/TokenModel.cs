using OGA.Core.BackEnd.Domain.Commons;
using Swashbuckle.AspNetCore.Annotations;

namespace OGA.AAS.BackEnd.Domain.Models;

public class TokenModel : BaseModel
{
    [SwaggerSchema(Description = "No maximum size")]
    public string? AccessToken { get; set; }

    [SwaggerSchema(Description = "Maximum length: 256")]
    public string? Email { get; set; }

    public string? ExpiredDate { get; set; }

    public string? RefreshToken { get; set; }
}
