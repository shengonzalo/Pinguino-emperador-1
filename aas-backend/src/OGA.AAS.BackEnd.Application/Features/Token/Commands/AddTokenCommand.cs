using MediatR;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Token.Commands
{
    public class AddTokenCommand : IRequest<OkResponseModel>
    {
        public TokenModel TokenModel { get; set; }

        public AddTokenCommand(TokenModel tokenModel)
        {
            TokenModel = tokenModel;
        }
    }
}
