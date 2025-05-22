using MediatR;
using OGA.AAS.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.User.Queries
{
    public class GetLastUserKeyByUserIdQuery : IRequest<UserKeyModel>
    {
        public int IdUser { get; set; }

        public GetLastUserKeyByUserIdQuery(int idUser)
        {
            IdUser = idUser;
        }
    }
}
