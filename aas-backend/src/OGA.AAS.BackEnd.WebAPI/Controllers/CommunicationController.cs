using Microsoft.AspNetCore.Mvc;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.Core.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models.Azure;
using OGA.Core.BackEnd.Infrastructure.Azure.Contracts;
using OGA.Core.BackEnd.WebAPI.Middleware.HttpResponseException;
using OGA.Core.BackEnd.WebAPI.Utilities;

namespace OGA.AAS.BackEnd.WebAPI.Controllers
{
    /// <summary>
    /// Se encarga de la gestión de las comuniaciones
    /// </summary>
    [ApiController]
    [Route("communications")]
    public class CommunicationController : ControllerBase
    {
        private readonly ICommunicationService _communicationService;
        private readonly IUser2FACodeService _user2FACodeService;

        public CommunicationController(IUser2FACodeService user2FACodeService, ICommunicationService communicationService) 
        {
            _user2FACodeService = user2FACodeService;
            _communicationService = communicationService;
        }

        /// <summary>
        /// Envía un email
        /// </summary>
        /// <param name="request">Datos para el envío del email</param>
        /// <returns>Resultado de la operación</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="400">Se lanzará esta respuesta cuando los datos de la request no sean correctos.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        //[SwaggerResponseExample(StatusCodes.Status200OK, typeof(OkResponseModelExample))]
        [Produces("application/json")]
        [HttpPost]
        //[Authorize]
        [Route("emails")]
        public async Task<IActionResult> Send2FAEmail([FromBody] EmailRequestModel request)
        {
            return Ok(await _communicationService.SendEmailAsync(request));
        }

        /// <summary>
        /// Envía un email con un código para 2FA
        /// </summary>
        /// <param name="recipient">Email del destinatario</param>
        /// <returns>Resultado de la operación</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="400">Se lanzará esta respuesta cuando los datos de la request no sean correctos.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        //[SwaggerResponseExample(StatusCodes.Status200OK, typeof(OkResponseModelExample))]
        [Produces("application/json")]
        [HttpPost]
        //[Authorize]
        [Route("emails/2fa")]
        public async Task<IActionResult> Send2FAEmail([FromQuery] string recipient)
        {
            var audit = ControllerUtility.GetAuditValues(ControllerContext);
            return Ok(await _user2FACodeService.Send2FAEmailAsync(recipient, audit));
        }

        /// <summary>
        /// Verifica un código 2FA
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <param name="code">Código a verificar</param>
        /// <returns>Resultado de la comprobación</returns>
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
        //[Authorize]
        [Route("2fa/verify")]
        public async Task<IActionResult> VerifyCode([FromQuery] int userId, [FromQuery] string code)
        {
            return Ok(await _user2FACodeService.VerifyCode(userId, code));
        }
    }
}
