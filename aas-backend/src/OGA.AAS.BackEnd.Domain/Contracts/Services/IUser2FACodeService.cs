using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Contracts.Services;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Domain.Contracts.Services
{
    public interface IUser2FACodeService : IBaseAsyncService<User2FACodeModel>
    {
        Task<OkResponseModel> Send2FAEmailAsync(string recipient, AuditModel audit);

        Task<bool> VerifyCode(int userId, string code);
    }
}
