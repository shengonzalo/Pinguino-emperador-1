using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Models
{
    public class UserKeyModel : BaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? PasswordHash { get; set; }
    }
}
