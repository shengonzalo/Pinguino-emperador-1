using OGA.AAS.BackEnd.Domain.Entities;

namespace OGA.AAS.BackEnd.Domain.Contracts.Persistence
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission?>> GetPermissionsByRoles(IEnumerable<int> roleId);
    }
}
