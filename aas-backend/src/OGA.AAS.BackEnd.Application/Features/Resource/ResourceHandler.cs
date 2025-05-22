using AutoMapper;
using MediatR;
using OGA.AAS.BackEnd.Application.Features.Resource.Queries;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Resource
{
    public class ResourceHandler : IRequestHandler<GetResourceByIdQuery, ResourceModel>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        public ResourceHandler(IResourceRepository resourceRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        public async Task<ResourceModel> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.GetResourceById(request.ResourceId, request.Nodes);
            return _mapper.Map<ResourceModel>(resource);
        }
    }
}
