using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OGA.Base.BackEnd.Domain.Contracts.Persistence;
using OGA.Base.BackEnd.Domain.Entities;
using OGA.Base.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Application.Features.BaseAsync;
using OGA.Core.BackEnd.Application.Features.BaseAsync.Commands;
using OGA.Core.BackEnd.Application.Features.BaseAsync.Queries;
using OGA.Core.BackEnd.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.Application.Registration;

[ExcludeFromCodeCoverage]
public static class ActionsAuditRegistration
{
    public static IServiceCollection AddActionsAuditServices(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<GetAllAsyncQuery<ActionsAuditModel>, IEnumerable<ActionsAuditModel>>,
            BaseAsyncHandler<ActionsAuditModel, IActionsAuditRepository, ActionsAudit>>();

        services.AddTransient<IRequestHandler<GetByIdAsyncQuery<ActionsAuditModel>, ActionsAuditModel>,
            BaseAsyncHandler<ActionsAuditModel, IActionsAuditRepository, ActionsAudit>>();

        services.AddTransient<IRequestHandler<AddAsyncCommand<ActionsAuditModel>, OkResponseModel>,
          BaseAsyncHandler<ActionsAuditModel, IActionsAuditRepository, ActionsAudit>>();

        services.AddTransient<IRequestHandler<UpdateAsyncCommand<ActionsAuditModel>, OkResponseModel>,
            BaseAsyncHandler<ActionsAuditModel, IActionsAuditRepository, ActionsAudit>>();

        services.AddTransient<IRequestHandler<DeleteAsyncCommand<ActionsAuditModel>, OkResponseModel>,
            BaseAsyncHandler<ActionsAuditModel, IActionsAuditRepository, ActionsAudit>>();

        return services;
    }
}
