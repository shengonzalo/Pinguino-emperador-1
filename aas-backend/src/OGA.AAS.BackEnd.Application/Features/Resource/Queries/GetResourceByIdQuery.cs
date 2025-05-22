using MediatR;
using OGA.AAS.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Resource.Queries
{
    public class GetResourceByIdQuery : IRequest<ResourceModel>
    {
        public int ResourceId { get; set; }

        public bool Nodes { get; set; }

        public GetResourceByIdQuery(int resourceId, bool nodes = false)
        {
            ResourceId = resourceId;
            Nodes = nodes;
        }
    }
}
