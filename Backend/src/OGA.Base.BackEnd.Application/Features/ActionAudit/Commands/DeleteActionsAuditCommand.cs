using MediatR;
using OGA.Base.BackEnd.Domain.Models;

namespace OGA.Base.BackEnd.Application.Features.ActionAudit.Commands;

public class DeleteActionsAuditCommand(IEnumerable<ActionsAuditModel> actionsAudits) : IRequest<bool>
{
    public IEnumerable<ActionsAuditModel> ActionsAudits { get; set; } = actionsAudits;
}
