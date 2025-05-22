using OGA.Core.BackEnd.Domain.Commons;
using Swashbuckle.AspNetCore.Annotations;

namespace OGA.AAS.BackEnd.Domain.Models
{
    public class PermissionModel : BaseModel
    {
        public int Id { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? Name { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? Description { get; set; }

        public bool Allowed { get; set; }

        public int ResourceId { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? ResourceName { get; set; }

        public PermissionTypeModel? PermissionType { get; set; }
    }
}
