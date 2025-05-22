using MediatR;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Permission.Commands
{
    public class UpdateRolePermGroupCommand : IRequest<IEnumerable<OkResponseModel>>
    {
        public IEnumerable<RolePermissionGroupModel> RolePermGroup { get; set; }

        public AuditModel Audit { get; set; }

        public UpdateRolePermGroupCommand(IEnumerable<RolePermissionGroupModel> rolePermGroup, AuditModel audit)
        {
            RolePermGroup = rolePermGroup;
            Audit = audit;
        }
    }
}
