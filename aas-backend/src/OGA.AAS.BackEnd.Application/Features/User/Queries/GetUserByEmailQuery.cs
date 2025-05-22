using MediatR;
using OGA.AAS.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.User.Queries
{
    public class GetUserByEmailQuery : IRequest<UserModel>
    {
        public string Email { get; set; }

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
