using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Application.Features.BaseAsync;
using OGA.Core.BackEnd.Application.Features.BaseAsync.Queries;

namespace OGA.AAS.BackEnd.Application.Registration
{
    public static class IdentifierTypeRegistration
    {
        public static IServiceCollection AddIdentifierTypeService(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetAllAsyncQuery<IdentifierTypeModel>, IEnumerable<IdentifierTypeModel>>,
                BaseAsyncHandler<IdentifierTypeModel, IIdentifierTypeRepository, IdentifierType>>();

            return services;
        }
    }
}
