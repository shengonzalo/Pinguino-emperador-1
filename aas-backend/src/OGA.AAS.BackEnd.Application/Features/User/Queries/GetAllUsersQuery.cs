using MediatR;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.User.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserModel>>
    {
        public PaginationModel Pagination { get; set; }

        public IEnumerable<OrderModel> Order { get; set; }

        public IEnumerable<FilterModel> Filter { get; set; }

        public GetAllUsersQuery(PaginationModel pagination, IEnumerable<OrderModel> order, IEnumerable<FilterModel> filter)
        {
            Pagination = pagination;
            Order = order;
            Filter = filter;
        }
    }
}
