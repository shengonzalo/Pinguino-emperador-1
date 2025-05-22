using MediatR;
using OGA.AAS.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Token.Queries;

public class GetTokenByEmailQuery : IRequest<TokenModel>
{
    public string Email { get; set; }

    public GetTokenByEmailQuery(string email)
    {
        Email = email;
    }
}
