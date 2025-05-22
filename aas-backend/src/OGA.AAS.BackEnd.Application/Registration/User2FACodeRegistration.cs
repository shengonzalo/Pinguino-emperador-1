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
    public static class User2FACodeRegistration
    {
        public static IServiceCollection AddUser2FACodeServices(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetAllAsyncQuery<User2FACodeModel>, IEnumerable<User2FACodeModel>>,
               BaseAsyncHandler<User2FACodeModel, IUser2FACodeRepository, User2FACode>>();

            services.AddTransient<IRequestHandler<GetByIdAsyncQuery<User2FACodeModel>, User2FACodeModel>,
                BaseAsyncHandler<User2FACodeModel, IUser2FACodeRepository, User2FACode>>();

            services.AddTransient<IRequestHandler<AddAsyncCommand<User2FACodeModel>, OkResponseModel>,
                BaseAsyncHandler<User2FACodeModel, IUser2FACodeRepository, User2FACode>>();

            services.AddTransient<IRequestHandler<UpdateAsyncCommand<User2FACodeModel>, OkResponseModel>,
                BaseAsyncHandler<User2FACodeModel, IUser2FACodeRepository, User2FACode>>();

            services.AddTransient<IRequestHandler<DeleteAsyncCommand<User2FACodeModel>, OkResponseModel>,
                BaseAsyncHandler<User2FACodeModel, IUser2FACodeRepository, User2FACode>>();

            return services;
        }
    }
}
