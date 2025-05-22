using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Contracts.Services;

namespace OGA.AAS.BackEnd.Domain.Contracts.Services
{
    public interface IMenuService : IBaseAsyncService<MenuModel>
    {
        Task<IEnumerable<MenuModel>> GetMenu();
    }
}
