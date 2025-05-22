using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;

namespace OGA.AAS.BackEnd.Domain.Contracts.Persistence
{
    public interface IUserKeyRepository : IBaseAsyncRepository<UserKey>
    {        
        Task<IEnumerable<UserKey?>> GetAllByUserIdAsync(int idUser);
        Task<IEnumerable<UserKey?>> GetLastThreeByUserIdAsync(int idUser, int lastKeys);
        Task<UserKey?> GetLastByUserIdAsync(int idUser);
    }
}
