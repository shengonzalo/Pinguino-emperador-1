using System.Globalization;
using AutoMapper;
using MediatR;
using OGA.AAS.BackEnd.Application.Features.Token.Commands;
using OGA.AAS.BackEnd.Application.Features.Token.Queries;
using OGA.AAS.BackEnd.Application.Registration;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Custom;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Enums;
using OGA.Core.BackEnd.Domain.Extensions;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.Token
{
    public class TokenHandler : 
        IRequestHandler<GetTokenByEmailQuery, TokenModel>,
        IRequestHandler<GetEnabledTokenQuery, bool>,
        IRequestHandler<GetTokenRefreshByEmailQuery, TokenModel>,
        IRequestHandler<GetTokenConfig, TokenConfigModel>,
        IRequestHandler<AddTokenCommand, OkResponseModel>, 
        IRequestHandler<AddRefreshTokenCommand, OkResponseModel>, 
        IRequestHandler<PutTokenCommand, OkResponseModel>,
        IRequestHandler<PutRefreshTokenCommand, OkResponseModel>
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IMapper _mapper;
        private readonly CurrentUserProvider _currentUserProvider;

        public TokenHandler(ITokenRepository tokenRepository, IMapper mapper, CurrentUserProvider currentUserProvider)
        {
            _tokenRepository = tokenRepository;
            _mapper = mapper;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<TokenModel> Handle(GetTokenByEmailQuery request, CancellationToken cancellationToken)
        {
            var token = await _tokenRepository.GetTokenByEmail(new List<string> { request.Email });

            return _mapper.Map<TokenModel>(token.FirstOrDefault());
        }

        public async Task<bool> Handle(GetEnabledTokenQuery request, CancellationToken cancellationToken)
        {
            var enabled = false;
            var tokens = await _tokenRepository.GetTokenByEmail(new List<string> { request.Email });
            if (tokens.Any())
            {
                var token = tokens.First();
                enabled = token.Enabled;
            }

            return enabled;
        }

        public async Task<TokenModel> Handle(GetTokenRefreshByEmailQuery request, CancellationToken cancellationToken)
        {
            var refreshToken = await _tokenRepository.GetRefeshTokenByEmail(request.Email);

            return _mapper.Map<TokenModel>(refreshToken);
        }

        public async Task<OkResponseModel> Handle(AddTokenCommand request, CancellationToken cancellationToken)
        {
            var token = _mapper.Map<Domain.Entities.Token>(request.TokenModel);
            token.Enabled = true;
            token.IUser = ConfigurationManager.WebAPIName;
            token.IDate = DateTime.UtcNow.ConvertDateTime();
            token.IComments = "Creación nuevo token";

            return await _tokenRepository.AddAsync(token);
        }

        public async Task<OkResponseModel> Handle(AddRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = _mapper.Map<Domain.Entities.Token>(request.TokenModel);

            token!.AccessToken = request.TokenModel.RefreshToken;
            token.IsRefreshToken = true;
            token.Enabled = true;
            token.IUser = ConfigurationManager.WebAPIName;
            token.IDate = DateTime.UtcNow.ConvertDateTime();
            token.IComments = "Creación nuevo token";

            return await _tokenRepository.AddAsync(token);
        }

        public async Task<OkResponseModel> Handle(PutTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new OkResponseModel
            {
                Id = 0,
                Message = "No había datos para actualizar"
            };

            var tokens = await _tokenRepository.GetTokenByEmail( request.ListEmail );
            foreach ( var token in tokens)
            {
                token.Enabled = false;
                token.UUser = ConfigurationManager.WebAPIName;
                token.UDate = DateTime.UtcNow.ConvertDateTime();
                token.UComments = "Invalidar token";

                response = await _tokenRepository.UpdateAsync(token);
            }
            
            return response;
        }

        public async Task<OkResponseModel> Handle(PutRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new OkResponseModel
            {
                Id = 0,
                Message = "No había datos para actualizar"
            };

            var token = await _tokenRepository.GetRefeshTokenByEmail(request.Email);
            if (token != null)
            {
                var currentUser = _currentUserProvider?.GetCurrentUser();
                var tz = currentUser != null ? currentUser.TimeZoneId : DateTimeEnum.Utc;

                token.ExpiredDate = DateTime.ParseExact(request.ExpiredDate, ConfigurationManager.LongDateFormat, CultureInfo.InvariantCulture)
                    .ConvertDateTime(tz);

                response = await _tokenRepository.UpdateAsync(token);
            }

            return response;
        }

        public async Task<TokenConfigModel> Handle(GetTokenConfig request, CancellationToken cancellationToken)
        {
            var tokenConfig = await _tokenRepository.GetTokenConfig();

            return _mapper.Map<TokenConfigModel>(tokenConfig);
        }
    }
}
