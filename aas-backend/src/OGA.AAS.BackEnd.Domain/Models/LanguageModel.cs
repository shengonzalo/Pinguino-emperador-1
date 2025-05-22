using OGA.Core.BackEnd.Domain.Commons;
using Swashbuckle.AspNetCore.Annotations;

namespace OGA.AAS.BackEnd.Domain.Models
{
    public class LanguageModel : BaseModel
    {
        public int Id { get; set; }

        [SwaggerSchema(Description = "Maximum length: 6")]
        public string? Code { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? Name { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? Description { get; set; }
    }    
}
