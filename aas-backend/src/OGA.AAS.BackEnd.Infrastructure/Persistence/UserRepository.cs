using Microsoft.EntityFrameworkCore;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Infrastructure.Context;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;
using OGA.Core.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Infrastructure.Logging.Contracts;
using OGA.Core.BackEnd.Infrastructure.Persistence;

namespace OGA.AAS.BackEnd.Infrastructure.Persistence;

public class UserRepository : BaseAsyncRepository<User>, IUserRepository
{
    public IBaseAsyncRepository<User> BaseRepository;

    public UserRepository(BaseDbContext context, ILoggingService logger) : base(context, logger)
    {
        BaseRepository = this;
    }

    public async Task<IEnumerable<User>> GetAllUsers(PaginationModel pagination, IEnumerable<OrderModel> order, IEnumerable<FilterModel> filter)
    {
        IEnumerable<User> users = await BaseRepository.GetAllAsync(pagination, order, filter);

        if (users.Any())
        {
            var ids = users.Select(x => x.Id).ToList();

            IEnumerable<UserRole> userRoles = await ((BaseDbContext)_context).UserRole!
               .Where(x => x.Enabled && ids.Contains(x.Id)).ToListAsync();

            var usrRolesIds = userRoles.Select(x => x.RoleId).ToList();

            IEnumerable<Role> roles = await ((BaseDbContext)_context).Role!
               .Where(x => x.Enabled && usrRolesIds.Contains(x.Id)).ToListAsync();

            var langIds = users.Select(x => x.LanguageId).Distinct().ToList();

            IEnumerable<Language> languages = await ((BaseDbContext)_context).Language!
               .Where(x => x.Enabled && langIds.Contains(x.Id)).ToListAsync();

            var identifierIds = users.Select(x => x.IdentifierTypeId).Distinct().ToList();

            IEnumerable<IdentifierType> identifiers = await ((BaseDbContext)_context).IdentifierType!
               .Where(x => x.Enabled && identifierIds.Contains(x.Id)).ToListAsync();

            foreach (var user in users)
            {
                var currentUsrRole = userRoles.Where(x => x.Id == user.Id).ToList();

                foreach (var rol in currentUsrRole)
                {
                    var currentRole = roles.FirstOrDefault(x => x.Id == rol.RoleId);
                    rol.Role = currentRole;
                }

                var currentLang = languages.FirstOrDefault(x => x.Id == user.LanguageId);

                var currentIdentifier = identifiers.FirstOrDefault(x => x.Id == user.IdentifierTypeId);

                user.UserRole = currentUsrRole;
                user.Language = currentLang;
                user.IdentifierType = currentIdentifier;
            }
        }

        return users;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await ((BaseDbContext)_context).User!
            .Include(l => l.Language)
            .Where(x => x.Email == email && x.Enabled && x.ActiveChk).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetUserByRol(int idRol)
    {
        return await ((BaseDbContext)_context).User!
            .Include(l => l.UserRole)
            .Where(x => x.UserRole!.Any(y => y.RoleId == idRol) && x.Enabled && x.ActiveChk).ToListAsync();
    }

    public async Task<OkResponseModel> UpdateUser(User user, IEnumerable<UserRole> userRoles)
    {
        await BaseRepository.UpdateAsync(user, false);

        var currentUserRoles = await((BaseDbContext)_context).UserRole!
           .Where(x => x.Id == user.Id).ToListAsync();

        foreach (var rol in userRoles)
        {
            var current = currentUserRoles.FirstOrDefault(x => x.RoleId == rol.RoleId);

            if (current == null)
            {
                ((BaseDbContext)_context).UserRole!.Add(new UserRole()
                {
                    User = user,
                    Id = user.Id,
                    RoleId = rol.RoleId,
                    IDate = user.UDate!.Value,
                    IUser = user.UUser,
                    IComments = user.UComments,
                    Enabled = true
                });
            }
            else if (!current.Enabled)
            {
                current.UDate = user.UDate;
                current.UUser = user.UUser;
                current.UComments = user.UComments;
                current.Enabled = true;

                ((BaseDbContext)_context).UserRole!.Attach(current);
                ((BaseDbContext)_context).Entry(current).State = EntityState.Modified;
            }
        }

        foreach (var rol in currentUserRoles)
        {
            var current = userRoles.FirstOrDefault(x => x.RoleId == rol.RoleId); 

            if (current == null)
            {
                rol.UDate = user.UDate;
                rol.UUser = user.UUser;
                rol.UComments = user.UComments;
                rol.Enabled = false;

                ((BaseDbContext)_context).UserRole!.Attach(rol);
                ((BaseDbContext)_context).Entry(rol).State = EntityState.Modified;
            }
        }

        await ((BaseDbContext)_context).SaveChangesAsync();

        var response = new OkResponseModel()
        {
            Id = user.Id,
            Message = "successful update"
        };
        return response;
    }

    public async Task<OkResponseModel> DeleteUser(User user)
    {
        await BaseRepository.UpdateAsync(user, false);

        var userRoles = await ((BaseDbContext)_context).UserRole!
           .Where(x => x.Enabled && x.Id == user.Id).ToListAsync();

        foreach (var rol in userRoles)
        {
            rol.UDate = user.UDate;
            rol.UUser = user.UUser;
            rol.UComments = user.UComments;
            rol.Enabled = false;

            ((BaseDbContext)_context).UserRole!.Attach(rol);
            ((BaseDbContext)_context).Entry(rol).State = EntityState.Modified;
        }

        await ((BaseDbContext)_context).SaveChangesAsync();

        var response = new OkResponseModel()
        {
            Id = user.Id,
            Message = "successful delete"
        };
        return response;
    }
}
