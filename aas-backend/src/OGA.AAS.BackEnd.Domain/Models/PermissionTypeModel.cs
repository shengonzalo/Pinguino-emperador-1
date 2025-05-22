using OGA.Core.BackEnd.Domain.Commons;
using Swashbuckle.AspNetCore.Annotations;

namespace OGA.AAS.BackEnd.Domain.Models
{
    public class PermissionTypeModel : BaseModel
    {
        public int Id { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? Description { get; set; }
    }
}
