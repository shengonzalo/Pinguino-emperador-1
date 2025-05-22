using MediatR;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.User.Commands
{
    public class CreateUserCommand : IRequest<OkResponseModel>
    {
        public UserModel Model { get; set; }

        public AuditModel Audit { get; set; }

        public bool Save { get; set; }

        public CreateUserCommand(UserModel model, AuditModel audit, bool save = true)
        {
            Model = model;
            Audit = audit;
            Save = save;
        }
    }
}
