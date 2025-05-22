using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Domain.Contracts.Persistence;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Domain.Contracts.Persistence
{
    public interface IUserRepository : IBaseAsyncRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsers(PaginationModel pagination, IEnumerable<OrderModel> order, IEnumerable<FilterModel> filter);

        Task<IEnumerable<User>> GetUserByRol(int idRol);

        Task<User?> GetUserByEmail(string email);

        Task<OkResponseModel> UpdateUser(User user, IEnumerable<UserRole> userRoles);

        Task<OkResponseModel> DeleteUser(User user);
    }
}
