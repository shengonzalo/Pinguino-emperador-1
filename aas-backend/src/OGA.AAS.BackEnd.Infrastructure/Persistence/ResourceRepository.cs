using Microsoft.EntityFrameworkCore;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Infrastructure.Context;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;
using OGA.Core.BackEnd.Infrastructure.Logging.Contracts;
using OGA.Core.BackEnd.Infrastructure.Persistence;

namespace OGA.AAS.BackEnd.Infrastructure.Persistence
{
    public class ResourceRepository : BaseAsyncRepository<Resource>, IResourceRepository
    {
        public IBaseAsyncRepository<Resource> BaseRepository;

        public ResourceRepository(BaseDbContext context, ILoggingService logger) : base(context, logger)
        {
            BaseRepository = this;
        }

        public async Task<Resource?> GetResourceById(int resourceId, bool nodes)
        {
            IQueryable<Resource> resource = ((BaseDbContext)_context).Resource!.Where(x =>
                x.Id == resourceId &&
                x.Enabled
            ).Select(x => x);
           
            if (nodes) 
            {
                resource = resource.Include(x => x.Nodes);
            }

            return await resource.FirstOrDefaultAsync();
        }
    }
}
