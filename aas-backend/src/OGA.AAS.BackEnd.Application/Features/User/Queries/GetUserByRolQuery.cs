using MediatR;
using OGA.AAS.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.User.Queries;

public class GetUserByRolQuery : IRequest<IEnumerable<UserModel>>
{
    public int IdRol { get; set; }

    public GetUserByRolQuery(int idRol)
    {
        IdRol = idRol;
    }
}
