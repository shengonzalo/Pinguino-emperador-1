using OGA.Base.BackEnd.Domain.Models.Internal.Graph;

namespace OGA.Base.BackEnd.Domain.ApiClient;

public interface IGraphRepository
{
    Task<MicrosoftUserModel?> ValidateMicrosoftTokenAsync(string token);
}
