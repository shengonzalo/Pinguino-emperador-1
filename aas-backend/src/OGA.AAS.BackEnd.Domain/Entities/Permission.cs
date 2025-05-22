using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool Allowed { get; set; }

        public int PermissionTypeId { get; set; }

        public int ResourceId { get; set; }

        public IEnumerable<PermissionGroup>? PermissionGroup { get; set; }

        public PermissionType? PermissionType { get; set; }

        public Resource? Resource { get; set; }
    }
}
