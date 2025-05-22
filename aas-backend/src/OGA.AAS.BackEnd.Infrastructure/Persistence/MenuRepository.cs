using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Infrastructure.Context;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;
using OGA.Core.BackEnd.Infrastructure.Logging.Contracts;
using OGA.Core.BackEnd.Infrastructure.Persistence;

namespace OGA.AAS.BackEnd.Infrastructure.Persistence
{
    public class MenuRepository : BaseAsyncRepository<Menu>, IMenuRepository
    {
        public IBaseAsyncRepository<Menu> BaseRepository;

        public MenuRepository(BaseDbContext context, ILoggingService logger) : base(context, logger)
        {
            BaseRepository = this;
        }
    }
}
