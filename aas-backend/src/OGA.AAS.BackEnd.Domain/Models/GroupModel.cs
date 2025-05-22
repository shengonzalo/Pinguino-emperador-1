using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Models
{
    public class GroupModel : BaseModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        public int Order { get; set; }

        public IEnumerable<RolePermissionGroupModel>? RolePermissionGroup { get; set; }
    }
}
