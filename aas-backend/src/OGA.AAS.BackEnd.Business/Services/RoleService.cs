using MediatR;
using OGA.AAS.BackEnd.Application.Features.Role.Commands;
using OGA.AAS.BackEnd.Application.Features.Role.Queries;
using OGA.AAS.BackEnd.Application.Features.User.Queries;
using OGA.AAS.BackEnd.Business.Resources;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Business.Services;
using OGA.Core.BackEnd.Domain.Exceptions;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Business.Services
{
    public class RoleService : BaseAsyncService<RoleModel>, IRoleService
    {
        public RoleService(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<RoleModel>> GetRolesByUser(string email)
        {
            var query = new GetRolesByUserQuery(email);
            var roles = await _mediator.Send(query);
            return roles;
        }

        public async Task<OkResponseModel> DeleteRole(int id, AuditModel audit)
        {
            var queryUser = new GetUserByRolQuery(id);
            var listUser = await _mediator.Send(queryUser);

            if (listUser != null && listUser.Any())
            {
                throw new PreconditionFailedException(RoleMessages.DeleteErrorUsers);
            }

            var command = new DeleteRoleCommand(id, audit);
            return await _mediator.Send(command);
        }
    }
}
