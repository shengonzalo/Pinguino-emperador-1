using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Application.Features.BaseAsync;
using OGA.Core.BackEnd.Application.Features.BaseAsync.Commands;
using OGA.Core.BackEnd.Application.Features.BaseAsync.Queries;
using OGA.Core.BackEnd.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace OGA.AAS.BackEnd.Application.Registration
{
    [ExcludeFromCodeCoverage]
    public static class RoleRegistration
    {
        public static IServiceCollection AddRoleServices(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetAllAsyncQuery<RoleModel>, IEnumerable<RoleModel>>,
               BaseAsyncHandler<RoleModel, IRoleRepository, Role>>();

            services.AddTransient<IRequestHandler<GetByIdAsyncQuery<RoleModel>, RoleModel>,
                BaseAsyncHandler<RoleModel, IRoleRepository, Role>>();

            services.AddTransient<IRequestHandler<AddAsyncCommand<RoleModel>, OkResponseModel>,
                BaseAsyncHandler<RoleModel, IRoleRepository, Role>>();

            services.AddTransient<IRequestHandler<UpdateAsyncCommand<RoleModel>, OkResponseModel>,
                BaseAsyncHandler<RoleModel, IRoleRepository, Role>>();

            services.AddTransient<IRequestHandler<DeleteAsyncCommand<RoleModel>, OkResponseModel>,
                BaseAsyncHandler<RoleModel, IRoleRepository, Role>>();

            return services;
        }
    }
}
