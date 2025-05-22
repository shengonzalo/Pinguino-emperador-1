using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.AAS.BackEnd.WebAPI.Controllers.Swagger;
using OGA.Core.BackEnd.WebAPI.Middleware.HttpResponseException;
using OGA.Core.BackEnd.WebAPI.Utilities;
using Swashbuckle.AspNetCore.Filters;

namespace OGA.AAS.BackEnd.WebAPI.Controllers
{
    /// <summary>
    /// Se encarga de la obtención de tokens JWT de la aplicación
    /// </summary>
    [ApiController]
    [Route("token")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Obtiene un token JWT existente en la aplicación
        /// </summary>
        /// <returns>Token JWT</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TokenModelExample))]
        [Produces("application/json")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Basic")]
        [Route("")]
        public async Task<IActionResult> GetToken()
        {
            var credentials = ControllerUtility.GetCredentials(ControllerContext);
            return Ok(await _tokenService.GetToken(credentials));
        }

        /// <summary>
        /// Obtiene un nuevo o existente token JWT para la aplicación
        /// </summary>
        /// <returns>Token JWT</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="400">Se lanzará esta respuesta cuando los datos de la request no sean correctos.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TokenModelExample))]
        [Produces("application/json")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Basic")]
        [Route("")]
        public async Task<IActionResult> PostToken()
        {
            var credentials = ControllerUtility.GetCredentials(ControllerContext);
            return Ok(await _tokenService.PostToken(credentials));
        }

        /// <summary>
        /// Valida un token JWT
        /// </summary>
        /// <returns>Valido o no</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="403">Se lanzará esta respuesta cuando el acceso al recurso esté prohibido.</response>
        /// <response code="404">Se lanzará esta respuesta cuando los datos de la request no hayan sido encontrados.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [Produces("application/json")]
        [HttpGet]
        [Authorize]
        [Route("valid")]
        public async Task<IActionResult> IsValidToken()
        {
            var tokenInfo = ControllerUtility.GetTokenInfo(ControllerContext);
            return Ok(await _tokenService.IsValidToken(tokenInfo));
        }

        /// <summary>
        /// Da de baja el token existente y crea uno nuevo
        /// </summary>
        /// <returns>Token JWT</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="400">Se lanzará esta respuesta cuando los datos de la request no sean correctos.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TokenModelExample))]
        [Produces("application/json")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Basic")]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var credentials = ControllerUtility.GetCredentials(ControllerContext);
            return Ok(await _tokenService.RefreshToken(credentials[0]));
        }

        /// <summary>
        /// Da de baja el token existente a partir de un email de usuario
        /// </summary>
        /// <returns>Valido o no</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="400">Se lanzará esta respuesta cuando los datos de la request no sean correctos.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [Produces("application/json")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Basic")]
        [Route(nameof(InvalidateTokenByEmail))]
        public async Task<IActionResult> InvalidateTokenByEmail()
        {
            var credentials = ControllerUtility.GetCredentials(ControllerContext);
            return Ok(await _tokenService.InvalidateTokenByEmail(credentials[0]));
        }

        /// <summary>
        /// Da de baja todos los tokens de usuario que tengan el rol enviado
        /// </summary>
        /// <returns>Valido o no</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="400">Se lanzará esta respuesta cuando los datos de la request no sean correctos.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [Produces("application/json")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Basic")]
        [Route(nameof(InvalidateTokenByRol))]
        public async Task<IActionResult> InvalidateTokenByRol([FromQuery] int idRol)
        {
            return Ok(await _tokenService.InvalidateTokenByRol(idRol));
        }
    }
}
