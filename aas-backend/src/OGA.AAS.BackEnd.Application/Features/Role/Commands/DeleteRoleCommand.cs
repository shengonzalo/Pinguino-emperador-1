using MediatR;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Role.Commands
{
    public class DeleteRoleCommand : IRequest<OkResponseModel>
    {
        public int Id { get; set; }

        public AuditModel Audit { get; set; }

        public DeleteRoleCommand(int id, AuditModel audit)
        {
            Id = id;
            Audit = audit;
        }
    }
}
