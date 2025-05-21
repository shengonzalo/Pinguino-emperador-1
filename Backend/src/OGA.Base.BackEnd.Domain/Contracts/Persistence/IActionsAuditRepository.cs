using OGA.Base.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;

namespace OGA.Base.BackEnd.Domain.Contracts.Persistence;

public interface IActionsAuditRepository : IBaseAsyncRepository<ActionsAudit>
{
    Task<bool> DeleteActionsAudit(IEnumerable<ActionsAudit> actionsAuditsIds);
}
