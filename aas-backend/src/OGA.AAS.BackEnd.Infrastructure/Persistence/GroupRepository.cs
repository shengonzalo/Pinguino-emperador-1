using Microsoft.EntityFrameworkCore;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Infrastructure.Context;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;
using OGA.Core.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Infrastructure.Logging.Contracts;
using OGA.Core.BackEnd.Infrastructure.Persistence;

namespace OGA.AAS.BackEnd.Infrastructure.Persistence
{
    public class GroupRepository : BaseAsyncRepository<Group>, IGroupRepository
    {
        public IBaseAsyncRepository<Group> BaseRepository;

        public GroupRepository(BaseDbContext context, ILoggingService logger) : base(context, logger)
        {
            BaseRepository = this;
        }

        public async Task<IEnumerable<Group>> GetAllGroups(PaginationModel pagination, IEnumerable<OrderModel> order, IEnumerable<FilterModel> filter)
        {
            IEnumerable<Group> groups = await BaseRepository.GetAllAsync(pagination, order, filter);

            if (groups.Any())
            {
                var ids = groups.Select(x => x.Id).ToList();

                IEnumerable<RolePermissionGroup> rolesGroups = await ((BaseDbContext)_context).RolePermissionGroup!
                   .Where(x => x.Enabled && ids.Contains(x.GroupId))
                   .Include(x => x.Role).ToListAsync();

                foreach (var group in groups)
                {
                    var currentRolesGroups = rolesGroups.Where(x => x.GroupId == group.Id).ToList();
                    group.RolePermissionGroup = currentRolesGroups;
                }
            }

            return groups;
        }
    }
}
