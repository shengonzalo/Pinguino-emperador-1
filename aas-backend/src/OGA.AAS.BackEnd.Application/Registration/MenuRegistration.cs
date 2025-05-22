using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Application.Features.BaseAsync;
using OGA.Core.BackEnd.Application.Features.BaseAsync.Queries;

namespace OGA.AAS.BackEnd.Application.Registration
{
    public static class MenuRegistration
    {
        public static IServiceCollection AddMenuServices(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetAllAsyncQuery<MenuModel>, IEnumerable<MenuModel>>,
                BaseAsyncHandler<MenuModel, IMenuRepository, Menu>>();

            return services;
        }
    }
}
