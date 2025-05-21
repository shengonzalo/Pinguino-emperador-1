using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Base.BackEnd.Application.Registration;
using OGA.Base.BackEnd.Domain.ApiClient;
using OGA.Base.BackEnd.Domain.Contracts.Services;
using OGA.Base.BackEnd.Domain.Enums;
using OGA.Base.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.Base.BackEnd.Business.Services;

public class SsoAuthService(IGraphRepository graphService, IUserService userService, ITokenService tokenService) : ISsoAuthService
{
    private readonly IUserService _userService = userService;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IGraphRepository _graphService = graphService;

    private const string _userCreate = "System";
    public async Task<TokenModel?> AuthenticateWithMicrosoft(SsoModel model)
    {
        // Validar el token con Microsoft Graph API
        var microsoftUser = await _graphService.ValidateMicrosoftTokenAsync(model.Token);
        if (microsoftUser == null) 
            return null;

        // Buscar usuario en la base de datos
        var user = await _userService.GetUserByEmail(microsoftUser.Email);
        // Crear usuario si no existe
        var correctBool = bool.TryParse(ConfigurationManager.CreateUserSSO, out bool createUserSSO);
        if (user == null && int.TryParse(ConfigurationManager.RolCreateSSO, out int idRol) && correctBool && createUserSSO)
        {
            user = new UserModel
            {
                Email = microsoftUser.Email,
                Name = microsoftUser.GivenName,
                Surname = microsoftUser.Surname,
                IdentifierTypeId = (int)IdentifierTypeEnum.NIF,
                Identifier = "--",
                PhoneNumber = microsoftUser.MobilePhone?.Trim(),
                LanguageId = (int)LanguageEnum.Espanol,
                ActiveChk = true,
                IsSystem = false,
                Roles = [new RoleModel { Id = idRol }],
                PasswordHash = ConfigurationManager.PasswordHash,
            };

            var dateNow = DateTime.Now;
            var audit = new AuditModel
            {
                IUser = _userCreate,
                IDate = dateNow,
                IComments = "Create by Microsoft SSO",
                UUser = _userCreate,
                UDate = dateNow,
                UComments = "Update by Microsoft SSO"
            };

            await _userService.CreateUser(user, audit);
        }
        else if(correctBool && !createUserSSO)
        {
            return null;
        }

        return await _tokenService.PostToken([user!.Email!], false);
    }
}
