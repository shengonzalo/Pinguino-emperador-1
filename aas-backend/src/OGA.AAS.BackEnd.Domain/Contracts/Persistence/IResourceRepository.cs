using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;

namespace OGA.AAS.BackEnd.Domain.Contracts.Persistence
{
    public interface IResourceRepository : IBaseAsyncRepository<Resource>
    {
        Task<Resource?> GetResourceById(int resourceId, bool nodes);
    }
}
