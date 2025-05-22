using MediatR;
using OGA.AAS.BackEnd.Application.Features.Permission.Commands;
using OGA.AAS.BackEnd.Application.Features.Permission.Queries;
using OGA.AAS.BackEnd.Application.Features.Resource.Queries;
using OGA.AAS.BackEnd.Application.Features.User.Queries;
using OGA.AAS.BackEnd.Business.Resources;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Exceptions;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Business.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IMediator _mediator;
        private readonly IRoleService _roleService;

        public PermissionService(IMediator mediator, IRoleService roleService)
        {
            _mediator = mediator;
            _roleService = roleService;
        }

        public async Task<UserPermissionModel> GetUserPermissions(int resourceId, string email)
        {
            var queryUser = new GetUserByEmailQuery(email);
            var user = await _mediator.Send(queryUser) ?? throw new NotFoundException(TokenMessages.UserNotFound);

            var queryResource = new GetResourceByIdQuery(resourceId, true);
            var resource = await _mediator.Send(queryResource) ?? throw new NotFoundException(PermissionMessages.ResourceNotFound);

            var roles = await _roleService.GetRolesByUser(email);
            var roleId = roles.Select(x => x.Id);

            var queryPerm = new GetPermissionsByRolesQuery(roleId);
            var permissions = await _mediator.Send(queryPerm);

            var nodes = new List<UserPermissionModel>();

            if (resource.Nodes != null)
            {
                foreach (var res in resource.Nodes)
                {
                    nodes.Add(new UserPermissionModel()
                    {
                        Resource = new ResourcePermissionModel()
                        {
                            Id = res.Id,
                            Name = res.Name,
                            Description = res.Description
                        },
                        Permissions = permissions.Where(x => x.ResourceId == res.Id),
                        Nodes = new List<UserPermissionModel>()
                    });
                }
            }

            return new UserPermissionModel()
            {
                Resource = new ResourcePermissionModel()
                {
                    Id = resource.Id,
                    Name = resource.Name,
                    Description = resource.Description
                },
                Permissions = permissions.Where(x => x.ResourceId == resource.Id),
                Nodes = nodes
            };
        }

        public async Task<DataPaginationModel<GroupModel>> GetAllGroups(QueryParamModel queryParam)
        {
            var pagination = queryParam.Pagination ?? new PaginationModel();
            var order = queryParam.Order != null && queryParam.Order.Any() ? queryParam.Order : new List<OrderModel>();
            var filter = queryParam.Filter != null && queryParam.Filter.Any() ? queryParam.Filter : new List<FilterModel>();

            var query = new GetPermissionsGroupsQuery(pagination, order, filter);
            var groups = await _mediator.Send(query);

            return new DataPaginationModel<GroupModel>()
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalSize = pagination.TotalSize,
                Data = groups
            };
        }

        public async Task<IEnumerable<OkResponseModel>> UpdateRolePermGroup(IEnumerable<RolePermissionGroupModel> rolePermGroup, AuditModel audit)
        {
            var command = new UpdateRolePermGroupCommand(rolePermGroup, audit);
            return await _mediator.Send(command);
        }
    }
}
