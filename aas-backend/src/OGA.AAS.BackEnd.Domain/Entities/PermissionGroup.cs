using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Entities
{
    public class PermissionGroup : BaseEntity
    {
        public int PermissionId { get; set; }

        public int GroupId { get; set; }

        public Permission? Permission { get; set; } = new Permission();

        public Group? Group { get; set; } = new Group();
    }
}
