using MediatR;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Token.Commands;

public class PutRefreshTokenCommand : IRequest<OkResponseModel>
{
    public string Email { get; set; }
    public string ExpiredDate { get; set; }

    public PutRefreshTokenCommand(string email, string expiredDate)
    {
        Email = email;
        ExpiredDate = expiredDate;
    }
}
