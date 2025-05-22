using MediatR;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Token.Commands;

public class PutTokenCommand : IRequest<OkResponseModel>
{
    public List<string> ListEmail { get; set; }

    public PutTokenCommand(List<string> listEmail)
    {
        ListEmail = listEmail;
    }
}
