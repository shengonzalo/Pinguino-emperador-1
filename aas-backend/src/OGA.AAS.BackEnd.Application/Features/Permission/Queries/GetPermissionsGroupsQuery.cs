using MediatR;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Permission.Queries
{
    public class GetPermissionsGroupsQuery : IRequest<IEnumerable<GroupModel>>
    {
        public PaginationModel Pagination { get; set; }

        public IEnumerable<OrderModel> Order { get; set; }

        public IEnumerable<FilterModel> Filter { get; set; }

        public GetPermissionsGroupsQuery(PaginationModel pagination, IEnumerable<OrderModel> order, IEnumerable<FilterModel> filter)
        {
            Pagination = pagination;
            Order = order;
            Filter = filter;
        }
    }
}
