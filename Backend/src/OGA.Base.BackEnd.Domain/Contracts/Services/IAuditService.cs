using OGA.Base.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Contracts.Services;

namespace OGA.Base.BackEnd.Domain.Contracts.Services;

public interface IAuditService : IBaseAsyncService<ActionsAuditModel>
{
    Task<bool> DeleteExpiredActionAudit(DateTime dateToCheck);
}
