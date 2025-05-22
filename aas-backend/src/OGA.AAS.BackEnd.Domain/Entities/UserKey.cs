using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Entities
{
    public class UserKey : BaseEntity
    {
        public string? PasswordHash { get; set; }
        public int IdUser { get; set; }
        public DateTime CreatedDate { get; set; }        
        public virtual User? User { get; set; }
    }
}
