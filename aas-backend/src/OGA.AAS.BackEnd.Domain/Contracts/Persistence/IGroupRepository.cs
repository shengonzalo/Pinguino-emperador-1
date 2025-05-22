using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Domain.Contracts.Persistence
{
    public interface IGroupRepository : IBaseAsyncRepository<Group>
    {
        Task<IEnumerable<Group>> GetAllGroups(PaginationModel pagination, IEnumerable<OrderModel> order, IEnumerable<FilterModel> filter);
    }
}
