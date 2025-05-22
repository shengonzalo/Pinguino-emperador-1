using Microsoft.EntityFrameworkCore;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Infrastructure.Context;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;
using OGA.Core.BackEnd.Domain.Extensions;
using OGA.Core.BackEnd.Infrastructure.Logging.Contracts;
using OGA.Core.BackEnd.Infrastructure.Persistence;

namespace OGA.AAS.BackEnd.Infrastructure.Persistence
{
    public class TokenRepository : BaseAsyncRepository<Token>, ITokenRepository
    {
        public IBaseAsyncRepository<Token> BaseRepository;

        public TokenRepository(BaseDbContext context, ILoggingService logger) : base(context, logger)
        {
            BaseRepository = this;
        }

        public async Task<List<Token>> GetTokenByEmail(List<string> listEmail)
        {
            var currentDt = DateTime.UtcNow.ConvertDateTime();
            return await ((BaseDbContext)_context).Token!.Where(x => listEmail.Contains(x.Email!) && x.ExpiredDate >= currentDt && x.Enabled && !x.IsRefreshToken)
                .OrderByDescending(x => x.ExpiredDate).ToListAsync();
        }

        public async Task<Token?> GetRefeshTokenByEmail(string email)
        {
            var currentDt = DateTime.UtcNow.ConvertDateTime();
            return await ((BaseDbContext)_context).Token!.Where(x => x.Email == email && x.ExpiredDate >= currentDt && x.Enabled && x.IsRefreshToken)
                .OrderByDescending(x => x.ExpiredDate).FirstOrDefaultAsync();
        }

        public async Task<TokenConfig?> GetTokenConfig()
        {
            return await ((BaseDbContext)_context).TokenConfig!.FirstOrDefaultAsync();
        }
    }
}
