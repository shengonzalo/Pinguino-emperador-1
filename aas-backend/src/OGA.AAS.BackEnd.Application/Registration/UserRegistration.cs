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
    public static class UserRegistration
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetAllAsyncQuery<UserModel>, IEnumerable<UserModel>>,
               BaseAsyncHandler<UserModel, IUserRepository, User>>();

            services.AddTransient<IRequestHandler<GetByIdAsyncQuery<UserModel>, UserModel>,
                BaseAsyncHandler<UserModel, IUserRepository, User>>();

            services.AddTransient<IRequestHandler<AddAsyncCommand<UserModel>, OkResponseModel>,
                BaseAsyncHandler<UserModel, IUserRepository, User>>();

            services.AddTransient<IRequestHandler<UpdateAsyncCommand<UserModel>, OkResponseModel>,
                BaseAsyncHandler<UserModel, IUserRepository, User>>();

            services.AddTransient<IRequestHandler<DeleteAsyncCommand<UserModel>, OkResponseModel>,
                BaseAsyncHandler<UserModel, IUserRepository, User>>();

            return services;
        }
    }
}
