using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Application.Features.BaseAsync;
using OGA.Core.BackEnd.Application.Features.BaseAsync.Queries;

namespace OGA.AAS.BackEnd.Application.Registration
{
    public static class LanguageRegistration
    {
        public static IServiceCollection AddLanguageService(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetAllAsyncQuery<LanguageModel>, IEnumerable<LanguageModel>>,
                BaseAsyncHandler<LanguageModel, ILanguageRepository, Language>>();

            return services;
        }
    }
}
