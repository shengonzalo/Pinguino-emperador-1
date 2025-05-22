using MediatR;
using OGA.AAS.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.User.Queries
{
    public class GetUserKeysByUserIdQuery : IRequest<IEnumerable<UserKeyModel>>
    {
        public int IdUser { get; set; }

        public GetUserKeysByUserIdQuery(int idUser)
        {
            IdUser = idUser;
        }
    }
}
