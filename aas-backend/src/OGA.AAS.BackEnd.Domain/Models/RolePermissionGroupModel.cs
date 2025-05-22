using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Models
{
    public class RolePermissionGroupModel : BaseModel
    {
        public int GroupId { get; set; }

        public int RoleId { get; set; }

        public string? RoleName { get; set; }

        public string? RoleDesc { get; set; }
    }
}
