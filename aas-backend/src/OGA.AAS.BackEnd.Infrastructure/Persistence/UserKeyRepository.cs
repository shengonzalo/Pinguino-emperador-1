using Microsoft.EntityFrameworkCore;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Infrastructure.Context;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;
using OGA.Core.BackEnd.Infrastructure.Logging.Contracts;
using OGA.Core.BackEnd.Infrastructure.Persistence;

namespace OGA.AAS.BackEnd.Infrastructure.Persistence;

public class UserKeyRepository : BaseAsyncRepository<UserKey>, IUserKeyRepository
{
    public IBaseAsyncRepository<UserKey> BaseRepository;

    public UserKeyRepository(BaseDbContext context, ILoggingService logger) : base(context, logger)
    {
        BaseRepository = this;
    }

    public async Task<IEnumerable<UserKey?>> GetAllByUserIdAsync(int idUser)
    {
        return await _context.Set<UserKey>().Where(u => u.IdUser == idUser && u.Enabled).ToListAsync();
    }

    public async Task<IEnumerable<UserKey?>> GetLastThreeByUserIdAsync(int idUser, int lastKeys)
    {
        return await _context.Set<UserKey>().Where(u => u.IdUser == idUser && u.Enabled).OrderByDescending(d=>d.CreatedDate).Take(lastKeys).ToListAsync();
    }

    public async Task<UserKey?> GetLastByUserIdAsync(int idUser)
    {
        return await _context.Set<UserKey>().Where(u => u.IdUser == idUser && u.Enabled).OrderByDescending(d => d.CreatedDate).FirstOrDefaultAsync();
    }
}
