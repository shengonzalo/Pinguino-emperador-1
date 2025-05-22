using MediatR;
using OGA.AAS.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Role.Queries
{
    public class GetRolesByUserQuery : IRequest<IEnumerable<RoleModel>>
    {
        public string Email { get; set; }

        public GetRolesByUserQuery(string email)
        {
            Email = email;
        }
    }
}
