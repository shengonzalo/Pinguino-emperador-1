using OGA.Core.BackEnd.Domain.Commons;
using Swashbuckle.AspNetCore.Annotations;

namespace OGA.AAS.BackEnd.Domain.Models
{
    public class UserModel : BaseModel
    {
        public int Id { get; set; }

        public int IdentifierTypeId { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? IdentifierTypeDesc { get; set; }

        [SwaggerSchema(Description = "Maximum length: 20")]
        public string? Identifier { get; set; }

        [SwaggerSchema(Description = "Maximum length: 100")]
        public string? Name { get; set; }

        [SwaggerSchema(Description = "Maximum length: 100")]
        public string? Surname { get; set; }

        [SwaggerSchema(Description = "Maximum length: 100")]
        public string? SecondSurname { get; set; }

        [SwaggerSchema(Description = "Maximum length: 20")]
        public string? PhoneNumber { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? Email { get; set; }

        [SwaggerSchema(Description = "No maximum size")]
        public string? PasswordHash { get; set; }

        public bool ActiveChk { get; set; }

        public bool IsSystem { get; set; }

        public int? LanguageId { get; set; }

        [SwaggerSchema(Description = "Maximum length: 6")]
        public string? LanguageCode { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? LanguageDesc { get; set; }

        public IEnumerable<RoleModel>? Roles { get; set; }

        public DateTime? PasswordExpiredDate { get; set; }
    }
}
