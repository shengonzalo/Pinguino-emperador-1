using Microsoft.EntityFrameworkCore;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Infrastructure.Context;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;
using OGA.Core.BackEnd.Infrastructure.Logging.Contracts;
using OGA.Core.BackEnd.Infrastructure.Persistence;

namespace OGA.AAS.BackEnd.Infrastructure.Persistence
{
    public class PermissionRepository : BaseAsyncRepository<Permission>, IPermissionRepository
    {
        public IBaseAsyncRepository<Permission> BaseRepository;

        public PermissionRepository(BaseDbContext context, ILoggingService logger) : base(context, logger)
        {
            BaseRepository = this;
        }

        public async Task<IEnumerable<Permission?>> GetPermissionsByRoles(IEnumerable<int> roleId)
        {
            var groupId = ((BaseDbContext)_context).RolePermissionGroup!.Where(x =>
                roleId.Contains(x.RoleId) &&
                x.Enabled
            ).Select(x => x.GroupId);

            var permissionId = ((BaseDbContext)_context).PermissionGroup!.Where(x =>
                groupId.Contains(x.GroupId) &&
                x.Enabled
            ).Select(x => x.PermissionId);
            
            var permissions = ((BaseDbContext)_context).Permission!.Where(x =>
                permissionId.Contains(x.Id) &&
                x.Enabled
            ).Include(x => x.PermissionType);

            return await permissions.ToListAsync();
        }
    }
}
