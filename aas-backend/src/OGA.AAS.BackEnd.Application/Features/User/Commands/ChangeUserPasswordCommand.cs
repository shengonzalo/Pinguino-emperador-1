using MediatR;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.User.Commands
{
    public class ChangeUserPasswordCommand : IRequest<OkResponseModel>
    {
        public int Id { get; set; }
        public string newPassword { get; set; }
        public AuditModel Audit { get; set; }
        public ChangeUserPasswordCommand(int id, string password, AuditModel audit)
        {
            Id = id;
            newPassword = password;
            Audit = audit;
        }
    }
}
