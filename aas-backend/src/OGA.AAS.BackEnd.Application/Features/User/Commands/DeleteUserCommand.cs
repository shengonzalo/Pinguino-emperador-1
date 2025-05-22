using MediatR;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.User.Commands
{
    public class DeleteUserCommand : IRequest<OkResponseModel>
    {
        public int Id { get; set; }

        public AuditModel Audit { get; set; }

        public DeleteUserCommand(int id, AuditModel audit)
        {
            Id = id;
            Audit = audit;
        }
    }
}
