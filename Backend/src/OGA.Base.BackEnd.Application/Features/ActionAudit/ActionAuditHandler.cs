using AutoMapper;
using MediatR;
using OGA.Base.BackEnd.Application.Features.ActionAudit.Commands;
using OGA.Base.BackEnd.Domain.Contracts.Persistence;
using OGA.Base.BackEnd.Domain.Entities;

namespace OGA.Base.BackEnd.Application.Features.ActionAudit;

public class ActionsAuditHandler(IMapper mapper,
    IActionsAuditRepository actionsAuditRepository
    ) : IRequestHandler<DeleteActionsAuditCommand, bool>
{
    private readonly IMapper _mapper = mapper;
    private readonly IActionsAuditRepository _actionsAuditRepository = actionsAuditRepository;

    public async Task<bool> Handle(DeleteActionsAuditCommand request, CancellationToken cancellationToken)
    {
        var entities = _mapper.Map<IEnumerable<ActionsAudit>>(request.ActionsAudits);
        var result = false;

        if (entities.Any() && entities != null)
            result = await _actionsAuditRepository.DeleteActionsAudit(entities);

        return result;
    }
}
