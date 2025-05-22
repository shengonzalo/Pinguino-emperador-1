using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public int RoleId { get; set; }

        public User? User { get; set; }

        public Role? Role { get; set; }
    }
}
