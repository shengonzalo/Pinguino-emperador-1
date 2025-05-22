using MediatR;
using OGA.AAS.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Token.Queries;

public class GetTokenConfig : IRequest<TokenConfigModel>
{
    public GetTokenConfig()
    {
    }
}
