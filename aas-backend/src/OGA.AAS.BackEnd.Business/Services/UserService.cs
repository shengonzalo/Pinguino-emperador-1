using FluentValidation.Results;
using MediatR;
using OGA.AAS.BackEnd.Application.Features.User.Commands;
using OGA.AAS.BackEnd.Application.Features.User.Queries;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.AAS.BackEnd.Domain.Resources.User;
using OGA.Core.BackEnd.Business.Services;
using OGA.Core.BackEnd.Domain.Contracts.Services;
using OGA.Core.BackEnd.Domain.Enums;
using OGA.Core.BackEnd.Domain.Exceptions;
using OGA.Core.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Infrastructure.Hash.Contracts;
using System.Text;
using System.Text.RegularExpressions;

namespace OGA.AAS.BackEnd.Business.Services;

public class UserService : BaseAsyncService<UserModel>, IUserService
{
    public IBaseAsyncService<UserModel> BaseService { get; set; }

    private readonly IHashService _hashService;
    private readonly IRoleService _roleService;
    private readonly ITokenService _tokenService;

    public UserService(IMediator mediator, IHashService hashService, IRoleService roleService, ITokenService tokenService) : base(mediator)
    {
        BaseService = this;

        _hashService = hashService;
        _roleService = roleService;
        _tokenService = tokenService;
    }

    public async Task<DataPaginationModel<UserModel>> GetAllUsers(QueryParamModel queryParam)
    {
        var pagination = queryParam.Pagination ?? new PaginationModel();
        var order = queryParam.Order != null && queryParam.Order.Any() ? queryParam.Order : new List<OrderModel>();
        var filter = queryParam.Filter != null && queryParam.Filter.Any() ? queryParam.Filter : new List<FilterModel>();

        var query = new GetAllUsersQuery(pagination, order, filter);
        var users = await _mediator.Send(query);

        return new DataPaginationModel<UserModel>()
        {
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize,
            TotalSize = pagination.TotalSize,
            Data = users
        };
    }

    public async Task<IEnumerable<UserModel>> GetUserByRol(int idRol)
    {
        var queryUser = new GetUserByRolQuery(idRol);
        var user = await _mediator.Send(queryUser);

        return user;
    }

    public async Task<OkResponseModel> CreateUser(UserModel model, AuditModel audit)
    {
        var user = await GetUserByEmail(model.Email!);

        if (user != null)
        {
            if (user.ActiveChk)
                throw new BadRequestException($"El email: {model.Email} ya pertenece a otro Usuario activo");

            model.ActiveChk = true;
            model.Id = user.Id;
            var Updatecommand = new UpdateUserCommand(user.Id, model, audit);

            var UpdateResp = await _mediator.Send(Updatecommand);
            UpdateResp.Message = "Usuario reactivado";
            return UpdateResp;
        }

        if(!CheckUserKey(model.PasswordHash!))
            throw new ValidationException(new List<ValidationFailure>() { new(nameof(RoleModel.Id), UserModelResources.NOTSECUREFORMATPASSWORD) });

        model.PasswordHash = GeneratePasswordHash(model.PasswordHash!);
         
        var command = new CreateUserCommand(model, audit);

        var resp= await _mediator.Send(command);
        resp.Message = "Usuario creado con exito";            

        return resp;
    }        

    public async Task<OkResponseModel> UpdateUser(int id, UserModel model, AuditModel audit)
    {           
        var queryUser = new GetUserByEmailQuery(model.Email!);
        var user = await _mediator.Send(queryUser);

        if (user != null && user.Id != model.Id)
        {
            throw new BadRequestException($"El email: {model.Email} ya pertenece a otro Usuario");
        }
        
        var current = await BaseService.GetByIdAsync(model.Id);
        var validTokenInfo = await CheckTokenInfo(current!, model);

        if (current != null && !validTokenInfo)
        {
            await _tokenService.InvalidateTokenByEmail(current.Email!);
        }

        var command = new UpdateUserCommand(id, model, audit);
        return await _mediator.Send(command);
    }

    public async Task<OkResponseModel> DeleteUser(int id, AuditModel audit)
    {          
        var command = new DeleteUserCommand(id, audit);
        return await _mediator.Send(command);
    }

    public async Task<OkResponseModel> ChangeUserPassword(string newPasswordHash, string useremail, AuditModel audit)
    {
        var user = await GetUserByEmail(useremail) ?? throw new BadRequestException($"El email: {useremail} no pertenece a ningun usuario");

        if (!CheckUserKey(newPasswordHash))
            throw new ValidationException(new List<ValidationFailure>() { new(nameof(RoleModel.Id), UserModelResources.NOTSECUREFORMATPASSWORD) });

        if (!user.IsSystem && await CheckIfKeyIsUsedAsync(user.Id, newPasswordHash))
            throw new ValidationException(new List<ValidationFailure>() { new(nameof(RoleModel.Id), UserModelResources.PASSWORDUSEDRECENTLY) });

        user.PasswordHash = GeneratePasswordHash(newPasswordHash);

        var command = new ChangeUserPasswordCommand(user.Id, user.PasswordHash, audit);

        return await _mediator.Send(command);
    }

    public async Task<OkResponseModel> ChangeUserPassword(string oldPasswordHash, string newPasswordHash, string useremail, AuditModel audit)
    {
        var user = await GetUserByEmail(useremail) ?? throw new BadRequestException($"El email: {useremail} no pertenece a ningun usuario");

        if (!_hashService.VerifyHashedPassword(user.PasswordHash, Encoding.UTF8.GetString(Convert.FromBase64String(oldPasswordHash))))
            throw new BadRequestException($"El password no corresponde con el del usurio {useremail}");        

        if (!CheckUserKey(newPasswordHash))
            throw new ValidationException(new List<ValidationFailure>() { new(nameof(RoleModel.Id), UserModelResources.NOTSECUREFORMATPASSWORD) });

        if (!user.IsSystem && await CheckIfKeyIsUsedAsync(user.Id, newPasswordHash))
            throw new ValidationException(new List<ValidationFailure>() { new(nameof(RoleModel.Id), UserModelResources.PASSWORDUSEDRECENTLY) });

        user.PasswordHash = GeneratePasswordHash(newPasswordHash);
        var command = new ChangeUserPasswordCommand(user.Id, user.PasswordHash!, audit);

        return await _mediator.Send(command);
    }
    public async Task<DateTime> GetKeyExpirationDateAsync(int idUser)
    {
        if (Application.Registration.ConfigurationManager.ConfigAuditUserKey == null)
            throw new BadRequestException("No está configurado el modelo para calcular la caducidad");

        var userkey = await GetLastUserKeyAsync(idUser);

        return userkey.CreatedDate.AddDays(Application.Registration.ConfigurationManager.ConfigAuditUserKey.ExpiredKeyInDays);
    }

    public async Task<UserModel> GetUserByEmail(string email)
    {
        var queryUser = new GetUserByEmailQuery(email);
        var user = await _mediator.Send(queryUser);

        return user;            
    }

    public async Task<UserModel?> GetUserByApiKey(string apiKey)
    {
        var query = new QueryParamModel()
        {
            Filter = new List<FilterModel>()
            {
                new()
                {
                    PropertyName = "ApiKey",
                    SearchText = apiKey,
                    Operator = OperatorEnum.Equals
                }
            }
        };

        var users = await GetAllUsers(query);

        if (users != null && users.Data != null && users.Data.Any())
        {
            return users.Data.FirstOrDefault();
        }

        return null;
    }

    private string GeneratePasswordHash(string passwordHash)
    {
        var password = Encoding.UTF8.GetString(Convert.FromBase64String(passwordHash));
        return _hashService.HashPassword(password);
    }

    private async Task<bool> CheckTokenInfo(UserModel current, UserModel model)
    {
        var validTokenInfo = true;

        if (current != null)
        {
            var hasToken = await _tokenService.GetEnabledToken(current.Email!);

            if (hasToken)
            {
                if (current.Email != model.Email || current.LanguageId != model.LanguageId)
                {
                    validTokenInfo = false;
                }
                else
                {
                    var roles = await _roleService.GetRolesByUser(current.Email!);

                    foreach (var role in roles)
                    {
                        var exists = model.Roles!.FirstOrDefault(x => x.Id == role.Id);
                        validTokenInfo = exists != null;

                        if (!validTokenInfo)
                        {
                            break;
                        }
                    }

                    if (validTokenInfo)
                    {
                        foreach (var role in model.Roles!)
                        {
                            var exists = roles.FirstOrDefault(x => x.Id == role.Id);
                            validTokenInfo = exists != null;

                            if (!validTokenInfo)
                            {
                                break;
                            }
                        }
                    }
                }
            }           
        }

        return validTokenInfo;
    }
    private static bool CheckUserKey(string passwordHash)
    {
        if (Application.Registration.ConfigurationManager.ConfigAuditUserKey == null || !Application.Registration.ConfigurationManager.ConfigAuditUserKey.UseRegex)
            return true;

        var password = Encoding.UTF8.GetString(Convert.FromBase64String(passwordHash));

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasMinimum8Chars = new Regex(@".{7,}");
        var noHasSpecialCharacter = new Regex("^[a-zA-Z0-9 ]*$");

        var isValidated = hasNumber.IsMatch(password) 
            && hasUpperChar.IsMatch(password) 
            && hasLowerChar.IsMatch(password) 
            && hasMinimum8Chars.IsMatch(password) 
            && !noHasSpecialCharacter.IsMatch(password);

        return isValidated;
    }
    private async Task<UserKeyModel> GetLastUserKeyAsync(int idUser)
    {
        var queryUser = new GetLastUserKeyByUserIdQuery(idUser);
        var userkey = await _mediator.Send(queryUser);

        return userkey;
    }
    private async Task<bool> CheckIfKeyIsUsedAsync(int idUser, string newPasswordHash)
    {
        if (Application.Registration.ConfigurationManager.ConfigAuditUserKey == null || Application.Registration.ConfigurationManager.ConfigAuditUserKey.ExpiredKeyInDays == 0)
            return false;

        var queryUser = new GetThreeLastsUserKeysByUserIdQuery(idUser, 3);
        var userkey = await _mediator.Send(queryUser);

        foreach (var key in userkey)
        {
            if (!_hashService.VerifyHashedPassword(key.PasswordHash!, Encoding.UTF8.GetString(Convert.FromBase64String(newPasswordHash))))
                continue;

            return true;
        }

        return false;
    }
}
