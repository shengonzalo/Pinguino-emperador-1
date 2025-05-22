using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Entities
{
    public class RolePermissionGroup : BaseEntity
    {
        public int GroupId { get; set; }

        public int RoleId { get; set; }

        public Group? Group { get; set; }

        public Role? Role { get; set; }
    }
}
