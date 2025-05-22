using OGA.Core.BackEnd.Domain.Commons;
using Swashbuckle.AspNetCore.Annotations;

namespace OGA.AAS.BackEnd.Domain.Models
{
    public class MenuModel : BaseModel
    {
        public int Id { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? Name { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? Description { get; set; }

        [SwaggerSchema(Description = "Maximum length: 25")]
        public string? Icon { get; set; }

        public int RootId { get; set; }

        public int? ParentId { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? Path { get; set; }

        public int Order { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? ElementId { get; set; }

        public IEnumerable<MenuModel>? MenuItems { get; set; }
    }
}
