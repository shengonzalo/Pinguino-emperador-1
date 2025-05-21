using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Base.BackEnd.Domain.Enums;
using OGA.Base.BackEnd.WebAPI.Controllers.Swagger;
using OGA.Base.BackEnd.WebAPI.Filters;
using OGA.Core.BackEnd.Domain.Models;
using OGA.Core.BackEnd.WebAPI.Middleware.HttpResponseException;
using OGA.Core.BackEnd.WebAPI.Utilities;
using Swashbuckle.AspNetCore.Filters;

namespace OGA.Base.BackEnd.WebAPI.Controllers
{
    /// <summary>
    /// Se encarga de la gestión de los usuarios
    /// </summary>
    [ApiController]
    [Route("users")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <param name="queryParam">Parametros para paginación, filtro y ordenación</param>
        /// <returns>Listado de usuarios</returns>
        /// <include file='docs_tags.xml' path='docs/tags[@name="getAll"]/*' />
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataPaginationModel<UserModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserPaginationModelExample))]
        [Produces("application/json")]
        [HttpGet]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> GetAllUsers([FromQuery] QueryParamModel queryParam)
        {
            return Ok(await _userService.GetAllUsers(queryParam));
        }

        /// <summary>
        /// Obtiene un usuario filtrando por su identificador
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Usuario</returns>
        /// <include file='docs_tags.xml' path='docs/tags[@name="getById"]/*' />
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserModelExample))]
        [Produces("application/json")]
        [HttpGet]
        [Authorize]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            return Ok(await _userService.GetByIdAsync(userId));
        }

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        /// <param name="user">Datos del usuario</param>
        /// <returns>Identificador del usuario</returns>
        /// <include file='docs_tags.xml' path='docs/tags[@name="create"]/*' />
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(OkResponseModelExample))]
        [Produces("application/json")]
        [HttpPost]
        [Authorize(Roles = "1")]
        [Route("")]
        [AuditFilter("User", AuditEnum.Create, typeof(UserModel))]
        public async Task<IActionResult> CreateUser([FromBody] UserModel user)
        {
            var audit = ControllerUtility.GetAuditValues(ControllerContext);
            return Ok(await _userService.CreateUser(user, audit));
        }

        /// <summary>
        /// Actualiza un usuario existente
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <param name="user">Datos del usuario</param>
        /// <returns>Identificador del usuario</returns>
        /// <include file='docs_tags.xml' path='docs/tags[@name="modify"]/*' />
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(OkResponseModelExample))]
        [Produces("application/json")]
        [HttpPut]
        [Authorize(Roles = "1")]
        [Route("{userId}")]
        [AuditFilter("User", AuditEnum.Update, typeof(UserModel))]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UserModel user)
        {
            var audit = ControllerUtility.GetAuditValues(ControllerContext);
            return Ok(await _userService.UpdateUser(userId, user, audit));
        }

        /// <summary>
        /// Elimina un usuario existente
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Identificador del usuario</returns>
        /// <include file='docs_tags.xml' path='docs/tags[@name="modify"]/*' />
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(OkResponseModelExample))]
        [Produces("application/json")]
        [HttpDelete]
        [Authorize(Roles = "1")]
        [Route("{userId}")]
        [AuditFilter("User", AuditEnum.Delete, typeof(UserModel))]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            var audit = ControllerUtility.GetAuditValues(ControllerContext);
            return Ok(await _userService.DeleteUser(userId, audit));
        }
    }
}
