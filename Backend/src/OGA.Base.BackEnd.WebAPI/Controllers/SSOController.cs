using Microsoft.AspNetCore.Mvc;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Base.BackEnd.Domain.Contracts.Services;
using OGA.Base.BackEnd.Domain.Models;
using OGA.Base.BackEnd.Domain.Resource.Sso;
using OGA.Base.BackEnd.WebAPI.Controllers.Swagger;
using OGA.Core.BackEnd.WebAPI.Middleware.HttpResponseException;
using Swashbuckle.AspNetCore.Filters;

namespace OGA.Base.BackEnd.WebAPI.Controllers;

/// <summary>
/// Controlador para la autenticación con Microsoft SSO
/// </summary>
[ApiController]
[Route("sso")]
public class SsoController(ISsoAuthService ssoAuthService) : ControllerBase
{
    private readonly ISsoAuthService _ssoAuthService = ssoAuthService;

    /// <summary>
    /// Autenticación con Microsoft y generación de token interno
    /// </summary>
    /// <param name="model">Token de Microsoft</param>
    /// <returns>Token JWT interno</returns>
    /// <response code="400">
    /// Se lanzará esta respuesta cuando los datos de la request no sean correctos. Los códigos de error personalizados son:
    /// TOKENMICROSOFTREQUIERED: El token de Microsoft es requerido.
    /// AUTHENTICATEFAIL: ha fallado la autenticación.
    /// </response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TokenModelExample))]
    [Produces("application/json")]
    [HttpPost("sign-in/microsoft")]
    public async Task<IActionResult> SignInWithMicrosoft([FromBody] SsoModel model)
    {
        if (string.IsNullOrEmpty(model.Token))
            return BadRequest(SsoResources.TOKENMICROSOFTREQUIERED);

        var internalToken = await _ssoAuthService.AuthenticateWithMicrosoft(model);
        if (internalToken == null)
            return Unauthorized(SsoResources.AUTHENTICATEFAIL);

        return Ok(internalToken);
    }
}
