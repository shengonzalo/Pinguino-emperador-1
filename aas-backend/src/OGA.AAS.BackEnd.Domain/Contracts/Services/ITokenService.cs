using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Domain.Contracts.Services
{
    public interface ITokenService
    {
        Task<TokenModel> GetToken(string[] credentials);

        Task<TokenModel> PostToken(string[] credentials, bool verify = true);

        Task<bool> IsValidToken(TokenInfoModel tokenInfo);

        Task<TokenModel> RefreshToken(string email);

        Task<OkResponseModel> InvalidateTokenByEmail(string email);

        Task<OkResponseModel> InvalidateTokenByRol(int idRol);

        Task<bool> GetEnabledToken(string email);
    }
}
