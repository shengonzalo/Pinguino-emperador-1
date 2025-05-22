using MediatR;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Token.Commands;

public class AddRefreshTokenCommand : IRequest<OkResponseModel>
{
    public TokenModel TokenModel { get; set; }

    public AddRefreshTokenCommand(TokenModel tokenModel)
    {
        TokenModel = tokenModel;
    }
}
