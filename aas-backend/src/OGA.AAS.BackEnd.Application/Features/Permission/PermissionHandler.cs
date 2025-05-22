using AutoMapper;
using MediatR;
using OGA.AAS.BackEnd.Application.Features.Permission.Commands;
using OGA.AAS.BackEnd.Application.Features.Permission.Queries;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Permission
{
    public class PermissionHandler : 
        IRequestHandler<GetPermissionsByRolesQuery, IEnumerable<PermissionModel>>,
        IRequestHandler<GetPermissionsGroupsQuery, IEnumerable<GroupModel>>,
        IRequestHandler<UpdateRolePermGroupCommand, IEnumerable<OkResponseModel>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IRolePermissionGroupRepository _rolePermissionGroupRepository;
        private readonly IMapper _mapper;

        public PermissionHandler(IPermissionRepository permissionRepository, IGroupRepository groupRepository,
            IRolePermissionGroupRepository rolePermissionGroupRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _groupRepository = groupRepository;
            _rolePermissionGroupRepository = rolePermissionGroupRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionModel>> Handle(GetPermissionsByRolesQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetPermissionsByRoles(request.RoleId);
            return _mapper.Map<IEnumerable<PermissionModel>>(permissions);
        }

        public async Task<IEnumerable<GroupModel>> Handle(GetPermissionsGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetAllGroups(request.Pagination, request.Order, request.Filter);
            return _mapper.Map<IEnumerable<GroupModel>>(groups);
        }

        public async Task<IEnumerable<OkResponseModel>> Handle(UpdateRolePermGroupCommand request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetAllGroups(new PaginationModel(), new List<OrderModel>(), new List<FilterModel>());

            var result = new List<OkResponseModel>();
            var addItems = new List<RolePermissionGroup>();
            var deleteItems = new List<RolePermissionGroup>();

            if (groups != null && groups.Any())
            {
                foreach (var group in groups)
                {
                    var current = request.RolePermGroup.Where(x => x.GroupId == group.Id).ToList();

                    if (current != null && current.Any())
                    {
                        foreach (var item in current)
                        {
                            var entity = _mapper.Map<RolePermissionGroup>(item);
                            entity.IDate = request.Audit.IDate;
                            entity.IUser = request.Audit.IUser;
                            entity.IComments = request.Audit.IComments;
                            entity.Enabled = true;

                            if (group.RolePermissionGroup != null && group.RolePermissionGroup.Any())
                            {
                                var exists = group.RolePermissionGroup.Any(x => x.GroupId == item.GroupId && x.RoleId == item.RoleId);

                                if (!exists)
                                {
                                    addItems.Add(entity);
                                }
                            }
                            else
                            {
                                addItems.Add(entity);
                            }
                        }
                    }

                    if (group.RolePermissionGroup != null && group.RolePermissionGroup.Any())
                    {
                        foreach (var item in group.RolePermissionGroup)
                        {
                            var entity = _mapper.Map<RolePermissionGroup>(item);

                            if (current != null && current.Any())
                            {
                                var exists = current.Any(x => x.GroupId == item.GroupId && x.RoleId == item.RoleId);

                                if (!exists)
                                {
                                    deleteItems.Add(entity);
                                }
                            }
                            else
                            {
                                deleteItems.Add(entity);
                            }
                        }
                    }
                }
            }

            if (addItems.Any())
            {
                foreach (var item in addItems)
                {
                    result.Add(await _rolePermissionGroupRepository.AddAsync(item));
                }
            }

            if (deleteItems.Any())
            {
                foreach (var item in deleteItems)
                {
                    result.Add(await _rolePermissionGroupRepository.DeleteAsync(item, logical: false));
                }
            }

            return result;
        }
    }
}
