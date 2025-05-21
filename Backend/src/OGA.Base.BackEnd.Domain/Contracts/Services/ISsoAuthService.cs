using OGA.AAS.BackEnd.Domain.Models;
using OGA.Base.BackEnd.Domain.Models;

namespace OGA.Base.BackEnd.Domain.Contracts.Services;

public interface ISsoAuthService
{
    Task<TokenModel?> AuthenticateWithMicrosoft(SsoModel model);
}
