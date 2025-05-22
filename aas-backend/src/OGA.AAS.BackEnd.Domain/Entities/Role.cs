using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }
        public int Order { get; set; }

        public IEnumerable<UserRole>? UserRole { get; set; }

        public IEnumerable<RolePermissionGroup>? RolePermissionGroup { get; set; }
    }
}
