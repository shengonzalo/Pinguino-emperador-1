using MediatR;
using OGA.AAS.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Token.Queries;

public class GetTokenRefreshByEmailQuery : IRequest<TokenModel>
{
    public string Email { get; set; }

    public GetTokenRefreshByEmailQuery(string email)
    {
        Email = email;
    }
}
