using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Entities
{
    public class User : BaseEntity
    {
        public int IdentifierTypeId { get; set; }

        public string? Identifier { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? SecondSurname { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public string? ApiKey { get; set; }

        public bool ActiveChk { get; set; }

        public bool IsSystem { get; set; }

        public int LanguageId { get; set; }

        public Language? Language { get; set; }

        public IdentifierType? IdentifierType { get; set; }

        public ICollection<UserRole>? UserRole { get; set; }

        public ICollection<UserKey>? UserKeys { get; set; }
    }
}
