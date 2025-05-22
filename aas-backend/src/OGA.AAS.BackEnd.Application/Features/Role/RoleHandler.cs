using AutoMapper;
using MediatR;
using OGA.AAS.BackEnd.Application.Features.Role.Commands;
using OGA.AAS.BackEnd.Application.Features.Role.Queries;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Application.Messages;
using OGA.Core.BackEnd.Domain.Exceptions;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Role
{
    public class RoleHandler : 
        IRequestHandler<GetRolesByUserQuery, IEnumerable<RoleModel>>,
        IRequestHandler<DeleteRoleCommand, OkResponseModel>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleModel>> Handle(GetRolesByUserQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetRolesByUser(request.Email);
            return _mapper.Map<IEnumerable<RoleModel>>(roles);
        }

        public async Task<OkResponseModel> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _roleRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(ValidatorMessages.NullEntity);

            entity.UDate = request.Audit.UDate;
            entity.UUser = request.Audit.UUser;
            entity.UComments = request.Audit.UComments;
            entity.Enabled = false;

            return await _roleRepository.DeleteRole(entity);
        }
    }
}
