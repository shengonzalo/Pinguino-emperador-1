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
    public class RoleRepository : BaseAsyncRepository<Role>, IRoleRepository
    {
        public IBaseAsyncRepository<Role> BaseRepository;

        public RoleRepository(BaseDbContext context, ILoggingService logger) : base(context, logger)
        {
            BaseRepository = this;
        }

        public async Task<IEnumerable<Role>?> GetRolesByUser(string email)
        {
            var roles = await ((BaseDbContext)_context).UserRole!.Where(x =>
                x.User != null &&
                x.User.Email == email &&
                x.User.Enabled &&
                x.Enabled
            ).Select(x => x.Role).Where(x => x != null && x.Enabled).ToListAsync();

            return roles!;
        }

        public async Task<OkResponseModel> DeleteRole(Role role)
        {
            await BaseRepository.UpdateAsync(role, false);

            var userRoles = await ((BaseDbContext)_context).UserRole!
               .Where(x => x.Enabled && x.RoleId == role.Id).ToListAsync();

            foreach (var rol in userRoles)
            {
                rol.UDate = role.UDate;
                rol.UUser = role.UUser;
                rol.UComments = role.UComments;
                rol.Enabled = false;

                ((BaseDbContext)_context).UserRole!.Attach(rol);
                ((BaseDbContext)_context).Entry(rol).State = EntityState.Modified;
            }

            await ((BaseDbContext)_context).SaveChangesAsync();
                       
            var response = new OkResponseModel()
            {
                Id = role.Id,
                Message = "successful delete"
            };
            return response;
        }
    }
}
