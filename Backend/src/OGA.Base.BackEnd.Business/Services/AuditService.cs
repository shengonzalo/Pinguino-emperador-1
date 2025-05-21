using MediatR;
using OGA.Base.BackEnd.Application.Features.ActionAudit.Commands;
using OGA.Base.BackEnd.Application.Registration;
using OGA.Base.BackEnd.Domain.Contracts.Services;
using OGA.Base.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Business.Services;
using OGA.Core.BackEnd.Domain.Contracts.Services;
using OGA.Core.BackEnd.Domain.Enums;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.Base.BackEnd.Business.Services;

public class AuditService : BaseAsyncService<ActionsAuditModel>, IAuditService
{
    public IBaseAsyncService<ActionsAuditModel> BaseService { get; set; }

    public AuditService(IMediator mediator) : base(mediator)
    {
        BaseService = this;
    }

    public async Task<bool> DeleteExpiredActionAudit(DateTime dateToCheck)
    {
        var dateToCheckParsed = dateToCheck.ToString(ConfigurationManager.GuionDateFormat);

        var queryParam = new QueryParamModel
        {
            Filter = [
                new FilterModel
                {
                    PropertyName = nameof(ActionsAuditModel.ExpiredDate),
                    SearchText = dateToCheckParsed,
                    Operator = OperatorEnum.LessThanOrEquals,
                    IsDate = true
                }
            ],
            Order = [],
            Pagination = new PaginationModel()
        };

        var actionsAudits = await BaseService.GetAllAsync(queryParam);
        var result = false;

        if (actionsAudits != null && actionsAudits.Data != null && actionsAudits.Data.Any())
        {
            var query = new DeleteActionsAuditCommand(actionsAudits.Data);
            result = await _mediator.Send(query);
        }

        return result;
    }
}
