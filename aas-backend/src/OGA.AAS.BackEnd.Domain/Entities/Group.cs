using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool Allowed { get; set; }
        public int Order { get; set; }

        public IEnumerable<PermissionGroup>? PermissionGroup { get; set; }

        public IEnumerable<RolePermissionGroup>? RolePermissionGroup { get; set; }
    }
}
