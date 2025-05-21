using OGA.Base.BackEnd.Domain.Contracts.Persistence;
using OGA.Base.BackEnd.Domain.Entities;
using OGA.Base.BackEnd.Infrastructure.Context;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;
using OGA.Core.BackEnd.Infrastructure.Logging.Contracts;
using OGA.Core.BackEnd.Infrastructure.Persistence;

namespace OGA.Base.BackEnd.Infrastructure.Persistence;

public class ActionsAuditRepository : BaseAsyncRepository<ActionsAudit>, IActionsAuditRepository
{
    public IBaseAsyncRepository<ActionsAudit> BaseRepository { get; set; }

    public ActionsAuditRepository(BaseDbContext context, ILoggingService logger) : base(context, logger)
    {
        BaseRepository = this;
    }
    public async Task<bool> DeleteActionsAudit(IEnumerable<ActionsAudit> actionsAudits)
    {
        _context.Set<ActionsAudit>().RemoveRange(actionsAudits);
        await _context.SaveChangesAsync();
        return true;
    }
}
