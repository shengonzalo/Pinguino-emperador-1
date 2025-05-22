using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;
using OGA.Core.BackEnd.Domain.Models;
using System.Data;

namespace OGA.AAS.BackEnd.Domain.Contracts.Persistence
{
    public interface IRoleRepository : IBaseAsyncRepository<Role>
    {
        Task<IEnumerable<Role>?> GetRolesByUser(string email);

        Task<OkResponseModel> DeleteRole(Role role);        
    }
}
