using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Contracts.Services;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Domain.Contracts.Services
{
    public interface IUserService : IBaseAsyncService<UserModel>
    {
        Task<DataPaginationModel<UserModel>> GetAllUsers(QueryParamModel queryParam);

        Task<IEnumerable<UserModel>> GetUserByRol(int idRol);

        Task<UserModel> GetUserByEmail(string email);

        Task<UserModel?> GetUserByApiKey(string apiKey);

        Task<OkResponseModel> CreateUser(UserModel model, AuditModel audit);

        Task<OkResponseModel> UpdateUser(int id, UserModel model, AuditModel audit);

        Task<OkResponseModel> ChangeUserPassword(string newPasswordHash, string useremail, AuditModel audit);

        Task<OkResponseModel> ChangeUserPassword(string oldPasswordHash,string newPasswordHash, string useremail, AuditModel audit);

        Task<OkResponseModel> DeleteUser(int id, AuditModel audit);

        Task<DateTime> GetKeyExpirationDateAsync(int idUser);
    }
}
