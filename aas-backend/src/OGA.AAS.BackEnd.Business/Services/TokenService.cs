using FluentValidation.Results;
using MediatR;
using OGA.AAS.BackEnd.Application.Features.Token.Commands;
using OGA.AAS.BackEnd.Application.Features.Token.Queries;
using OGA.AAS.BackEnd.Application.Features.User.Queries;
using OGA.AAS.BackEnd.Application.Registration;
using OGA.AAS.BackEnd.Business.Resources;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Custom;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.AAS.BackEnd.Domain.Resources.User;
using OGA.Core.BackEnd.Domain.Enums;
using OGA.Core.BackEnd.Domain.Exceptions;
using OGA.Core.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Infrastructure.Hash.Contracts;
using OGA.Core.BackEnd.Infrastructure.Jwt.Contracts;
using OGA.Core.BackEnd.Infrastructure.Jwt.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace OGA.AAS.BackEnd.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly IMediator _mediator;
        private readonly IJwtService _jwtService;
        private readonly IHashService _hashService;
        private readonly IRoleService _roleService;
        private readonly CurrentUserProvider _currentUserProvider;

        public TokenService(IMediator mediator, IJwtService jwtService, IHashService hashService, 
            IRoleService roleService, CurrentUserProvider currentUserProvider)
        {
            _mediator = mediator;
            _jwtService = jwtService;
            _hashService = hashService;
            _roleService = roleService;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<TokenModel> GetToken(string[] credentials)
        {
            var queryToken = new GetTokenByEmailQuery(credentials[0]);
            var token = await _mediator.Send(queryToken);

            if (token == null)
            {
                throw new NotFoundException(TokenMessages.TokenNotFound);
            }
            else
            {
                var queryRefreshToken = new GetTokenRefreshByEmailQuery(credentials[0]);
                var responseRefreshToken = await _mediator.Send(queryRefreshToken);

                if(responseRefreshToken != null)
                    token.RefreshToken = responseRefreshToken.AccessToken;
            }

            return token;
        }

        public async Task<TokenModel> PostToken(string[] credentials, bool verify = true)
        {
            var queryUser = new GetUserByEmailQuery(credentials[0]);
            var user = await _mediator.Send(queryUser) ?? throw new NotFoundException(TokenMessages.UserNotFound);

            if (verify && !_hashService.VerifyHashedPassword(user.PasswordHash, credentials[1]))
            {
                throw new UnauthorizedException(TokenMessages.IncorrectPassword);
            }

            if(!user.IsSystem && await CheckIfIsExpiredUserKeyAsync(user.Id))
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(RoleModel.Id), UserModelResources.PASSWORDEXPIRED) });

            TokenModel? token;

            try
            {
                token = await GetToken(credentials);
            }
            catch (NotFoundException)
            {
                token = await CreateToken(credentials[0], user);
            }

            return token;
        }

        public async Task<bool> IsValidToken(TokenInfoModel tokenInfo)
        {
            var isValid = await GetEnabledToken(tokenInfo.Email!);

            if (isValid)
            {
                var queryUser = new GetUserByEmailQuery(tokenInfo.Email!);
                var user = await _mediator.Send(queryUser) ?? throw new NotFoundException(TokenMessages.UserNotFound);

                var roles = await _roleService.GetRolesByUser(tokenInfo.Email!);
                if (tokenInfo.Email != user.Email || tokenInfo.UserId != user.Id.ToString() || tokenInfo.LanguageCode != user.LanguageCode)
                {
                    isValid = false;
                }
                else
                {
                    foreach (var role in tokenInfo.RoleId!)
                    {
                        var current = roles.FirstOrDefault(x => x.Id.ToString() == role);
                        isValid = current != null;

                        if (!isValid)
                        {
                            break;
                        }
                    }

                    if (isValid)
                    {
                        foreach (var role in roles)
                        {
                            var current = tokenInfo.RoleId!.FirstOrDefault(x => x == role.Id.ToString());
                            isValid = current != null;

                            if (!isValid)
                            {
                                break;
                            }
                        }
                    }
                }
            }            

            return isValid;
        }

        public async Task<TokenModel> RefreshToken(string email)
        {
            var queryUser = new GetUserByEmailQuery(email);
            var user = await _mediator.Send(queryUser);

            await InvalidateTokenByEmail(email);

            if (!user.IsSystem && await CheckIfIsExpiredUserKeyAsync(user.Id))
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(RoleModel.Id), UserModelResources.PASSWORDEXPIRED) });

            return await CreateToken(email, user);
        }

        public async Task<OkResponseModel> InvalidateTokenByEmail(string email)
        {
            var command = new PutTokenCommand(new List<string> { email });
            return await _mediator.Send(command);
        }

        public async Task<OkResponseModel> InvalidateTokenByRol(int idRol)
        {
            var queryUser = new GetUserByRolQuery(idRol);
            var listUser = await _mediator.Send(queryUser);

            var command = new PutTokenCommand(listUser.Select(x => x.Email).ToList()!);
            return await _mediator.Send(command);
        }

        public async Task<bool> GetEnabledToken(string email)
        {
            var queryEnabled = new GetEnabledTokenQuery(email);
            return await _mediator.Send(queryEnabled);
        }

        private async Task<TokenModel> CreateToken(string email, UserModel user)
        {
            var roles = await _roleService.GetRolesByUser(email);
            var claims = new List<Claim>
            {
                new Claim("roles", JsonSerializer.Serialize(roles.Select(i => i.Id.ToString())), JsonClaimValueTypes.JsonArray),
                new Claim("uid", user.Id.ToString()),
                new Claim("langcode", user.LanguageCode!)
            };

            var minute = ConfigurationManager.JwtMinutes;
            var commandTokenConfig = new GetTokenConfig();
            var tokenConfig = await _mediator.Send(commandTokenConfig);
            if (tokenConfig is not null && tokenConfig.Minute is not null)
                minute = tokenConfig.Minute.ToString();

            var currentUser = _currentUserProvider?.GetCurrentUser();
            var tz = currentUser != null ? currentUser.TimeZoneId : DateTimeEnum.Utc;

            var response = _jwtService.GenerateJwt(new JwtRequestModel()
            {
                Email = email,
                SecretKey = ConfigurationManager.JwtKey,
                Issuer = ConfigurationManager.JwtIssuer,
                Audience = ConfigurationManager.JwtAudience,
                ExpirationMinutes = double.Parse(minute!),
                TzId = tz,
                Claims = claims
            });

            var token = new TokenModel()
            {
                AccessToken = response.AccessToken,
                Email = response.Email,
                ExpiredDate = response.ExpiredDate
            };

            var command = new AddTokenCommand(token);
            await _mediator.Send(command);

            var queryRefreshToken = new GetTokenRefreshByEmailQuery(email);
            var resultRefreshToken = await _mediator.Send(queryRefreshToken);

            if(resultRefreshToken != null)
            {
                token.RefreshToken = resultRefreshToken.AccessToken;

                var commandPutRefresh = new PutRefreshTokenCommand(resultRefreshToken.Email!, token.ExpiredDate!);
                await _mediator.Send(commandPutRefresh);
            }
            else
            {
                var refreshToken = _jwtService.GenerateJwt(new JwtRequestModel()
                {
                    Email = email,
                    SecretKey = ConfigurationManager.JwtKey,
                    Issuer = ConfigurationManager.JwtIssuer,
                    Audience = ConfigurationManager.JwtAudience,
                    ExpirationMinutes = double.Parse(ConfigurationManager.JwtMinutes),
                    TzId = tz
                });

                token.RefreshToken = refreshToken.AccessToken;

                var commandRefresh = new AddRefreshTokenCommand(token);
                await _mediator.Send(commandRefresh);
            }

            return token;
        }
        private async Task<bool> CheckIfIsExpiredUserKeyAsync(int idUser)
        {
            if (ConfigurationManager.ConfigAuditUserKey == null || ConfigurationManager.ConfigAuditUserKey.ExpiredKeyInDays == 0)
                return false;

            var queryUser = new GetLastUserKeyByUserIdQuery(idUser);
            var userkey = await _mediator.Send(queryUser);

            if (userkey == null)
                return false;

            return userkey.CreatedDate.AddDays(ConfigurationManager.ConfigAuditUserKey.ExpiredKeyInDays) < DateTime.UtcNow;
        }
    }
}
