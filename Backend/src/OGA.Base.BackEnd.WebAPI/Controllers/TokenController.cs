using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Base.BackEnd.WebAPI.Controllers.Swagger;
using OGA.Core.BackEnd.WebAPI.Middleware.HttpResponseException;
using OGA.Core.BackEnd.WebAPI.Utilities;
using Swashbuckle.AspNetCore.Filters;

namespace OGA.Base.BackEnd.WebAPI.Controllers
{
    /// <summary>
    /// Se encarga de la obtención de tokens JWT de la aplicación
    /// </summary>
    [ApiController]
    [Route("token")]
    public class TokenController(ITokenService tokenService) : ControllerBase
    {
        private readonly ITokenService _tokenService = tokenService;

        /// <summary>
        /// Obtiene un token JWT existente en la aplicación
        /// </summary>
        /// <returns>Token JWT</returns>
        /// <include file='docs_tags.xml' path='docs/tags[@name="getAll"]/*' />
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
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
        /// <include file='docs_tags.xml' path='docs/tags[@name="create"]/*' />
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
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
        /// <include file='docs_tags.xml' path='docs/tags[@name="getById"]/*' />
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
        /// <include file='docs_tags.xml' path='docs/tags[@name="modify"]/*' />
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TokenModelExample))]
        [Produces("application/json")]
        [HttpPut]
        [Authorize]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var tokenInfo = ControllerUtility.GetTokenInfo(ControllerContext);
            return Ok(await _tokenService.RefreshToken(tokenInfo.Email!));
        }
    }
}
