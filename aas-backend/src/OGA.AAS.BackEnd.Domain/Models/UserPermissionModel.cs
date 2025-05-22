using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Models
{
    public class UserPermissionModel : BaseModel
    {
        public ResourcePermissionModel? Resource { get; set; }

        public IEnumerable<PermissionModel>? Permissions { get; set; }

        public IEnumerable<UserPermissionModel>? Nodes { get; set; }
    }
}
