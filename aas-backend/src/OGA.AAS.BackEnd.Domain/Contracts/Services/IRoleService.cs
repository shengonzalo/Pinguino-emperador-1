using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Contracts.Services;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Domain.Contracts.Services
{
    public interface IRoleService : IBaseAsyncService<RoleModel>
    {
        Task<IEnumerable<RoleModel>> GetRolesByUser(string email);

        Task<OkResponseModel> DeleteRole(int id, AuditModel audit);
    }
}
