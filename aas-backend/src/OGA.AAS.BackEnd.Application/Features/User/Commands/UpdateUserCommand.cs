using MediatR;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.User.Commands
{
    public class UpdateUserCommand : IRequest<OkResponseModel>
    {
        public int Id { get; set; }

        public UserModel Model { get; set; }

        public AuditModel Audit { get; set; }

        public UpdateUserCommand(int id, UserModel model, AuditModel audit)
        {
            Id = id;
            Model = model;
            Audit = audit;
        }
    }
}
