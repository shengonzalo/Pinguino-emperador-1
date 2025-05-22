using MediatR;

namespace OGA.AAS.BackEnd.Application.Features.Token.Queries;

public class GetEnabledTokenQuery : IRequest<bool>
{
    public string Email { get; set; }

    public GetEnabledTokenQuery(string email)
    {
        Email = email;
    }
}
