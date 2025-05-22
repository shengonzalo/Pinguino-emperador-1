using MediatR;
using OGA.AAS.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Permission.Queries
{
    public class GetPermissionsByRolesQuery : IRequest<IEnumerable<PermissionModel>>
    {
        public IEnumerable<int> RoleId { get; set; }

        public GetPermissionsByRolesQuery(IEnumerable<int> roleId)
        {
            RoleId = roleId;
        }
    }
}
