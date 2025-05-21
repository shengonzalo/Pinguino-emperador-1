using Acesur.Aps.BackEnd.Application.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using OGA.Base.BackEnd.Domain.Contracts.Services;
using OGA.Base.BackEnd.Domain.Enums;
using OGA.Core.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Infrastructure.Logging.Contracts;
using OGA.Core.BackEnd.WebAPI.Utilities;
using System.Reflection;
using System.Security.Claims;

namespace OGA.Base.BackEnd.WebAPI.Filters;

public class AuditFilterAttribute(
     string entityName,
     AuditEnum auditEnum,
     Type serviceType,
     string methodName = "GetByIdAsync",
     Type[]? types = null,
     bool useIdAsParam = true) : ActionFilterAttribute
{
    private readonly string _entityName = entityName;
    private readonly AuditEnum _auditEnum = auditEnum;
    private readonly Type _serviceType = serviceType;
    private readonly string methodName = methodName;
    private readonly Type[]? _types = types;
    private readonly bool _useIdAsParam = useIdAsParam;

    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var serviceProvider = context.HttpContext.RequestServices;
        OkResponseModel? ofResponseMdel = new();
        string? useremail = null;
        try
        {

            if (((ObjectResult)context.Result)?.Value is OkResponseModel)
            {
                ofResponseMdel = ((ObjectResult)context.Result)?.Value as OkResponseModel;

                var _auditService = serviceProvider.GetRequiredService<IAuditService>();

                var serviceType = serviceProvider.GetRequiredService(_serviceType);

                MethodInfo? method = _useIdAsParam ?
                    serviceType.GetType().GetMethod(methodName, _types ?? ([typeof(int), typeof(BusinessPermModel), typeof(bool?)])) :
                    serviceType.GetType().GetMethod(methodName, _types!) ??
                    throw new MethodAccessException("El método no existe o ha cambiado");

                var claimsIdentity = context.HttpContext?.User?.Identity as ClaimsIdentity;
                useremail = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? claimsIdentity?.FindFirst(ClaimTypes.Name)?.Value;


                object?[]? filters = null;
                if (_useIdAsParam)
                    filters = methodName == "GetByIdAsync" ? [ofResponseMdel!.Id, null, null] : [ofResponseMdel!.Id];

                var data = await method!.InvokeAsync(serviceType, _useIdAsParam ? filters : null);

                var audit = ControllerUtility.GetAuditValues(((ControllerBase)context.Controller).ControllerContext);
                await _auditService.AddAsync(new Domain.Models.ActionsAuditModel
                {
                    IdAction = (int)_auditEnum,
                    ActionName = _auditEnum.ToString(),
                    EntityName = _entityName,
                    ActionDate = DateTime.UtcNow,
                    ControllerName = context.Controller.GetType().Name,
                    UserName = useremail,
                    IdEntity = ofResponseMdel!.Id,
                    InfoAction = data == null ? string.Empty : JsonConvert.SerializeObject(data)
                }, audit);


            }
        }
        catch (Exception ex)
        {
            var logger = serviceProvider.GetRequiredService<ILoggingService>();

            string message = string.Concat(ex.GetType().Name, " - ", ex.Message);
            if (ex.InnerException != null)
                message = string.Concat(message, ex.InnerException.Message);

            object?[] parameters = [ ofResponseMdel == null ? global::System.Array.Empty<object>()
                : ofResponseMdel.Id, useremail ?? string.Empty ];
            logger.Error(message, nameof(AuditFilterAttribute), $"{_auditEnum} - {_entityName}", 0, parameters!);
        }

        await base.OnResultExecutionAsync(context, next);
    }
}
