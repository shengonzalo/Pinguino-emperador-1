using OGA.Core.BackEnd.Domain.Commons;
using Swashbuckle.AspNetCore.Annotations;

namespace OGA.AAS.BackEnd.Domain.Models
{
    public class ChangePasswordUserModel:BaseModel
    {
        [SwaggerSchema(Description = "No maximum size")]
        public string? OldPasswordHash { get; set; }

        [SwaggerSchema(Description = "No maximum size")]
        public string? NewPasswordHash { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? Email { get; set; }
    }
}
