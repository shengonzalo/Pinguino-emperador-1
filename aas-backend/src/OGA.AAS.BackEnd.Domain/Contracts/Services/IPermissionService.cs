using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Domain.Contracts.Services
{
    public interface IPermissionService
    {
        Task<UserPermissionModel> GetUserPermissions(int resourceId, string email);

        Task<DataPaginationModel<GroupModel>> GetAllGroups(QueryParamModel queryParam);

        Task<IEnumerable<OkResponseModel>> UpdateRolePermGroup(IEnumerable<RolePermissionGroupModel> rolePermGroup, AuditModel audit);
    }
}
