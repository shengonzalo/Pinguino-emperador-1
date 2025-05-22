using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;

namespace OGA.AAS.BackEnd.Domain.Contracts.Persistence
{
    public interface ITokenRepository : IBaseAsyncRepository<Token>
    {
        Task<List<Token>> GetTokenByEmail(List<string> listEmail);

        Task<Token?> GetRefeshTokenByEmail(string email);

        Task<TokenConfig?> GetTokenConfig();
    }
}
