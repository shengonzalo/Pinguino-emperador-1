using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OGA.Base.BackEnd.WebAPI.Controllers.Swagger;
using OGA.Core.BackEnd.Domain.Contracts.Services;
using OGA.Core.BackEnd.Domain.Models;
using OGA.Core.BackEnd.WebAPI.Middleware.HttpResponseException;
using OGA.Core.BackEnd.WebAPI.Utilities;
using Swashbuckle.AspNetCore.Filters;

namespace OGA.Base.BackEnd.WebAPI.Controllers
{
    /// <summary>
    /// Se encarga de la gestión de los permisos de negocio
    /// </summary>
    [ApiController]
    [Route("businessperms")]
    public class BusinessPermController(IBusinessEntityService businessEntityService, IUserBusinessPermService userBusinessPermService) : ControllerBase
    {
        private readonly IBusinessEntityService _businessEntityService = businessEntityService;
        private readonly IUserBusinessPermService _userBusinessPermService = userBusinessPermService;

        /// <summary>
        /// Obtiene todas entidades de negocio
        /// </summary>
        /// <param name="queryParam">Parametros para paginación, filtro y ordenación</param>
        /// <returns>Listado de entidades de negocio</returns>
        /// <include file='docs_tags.xml' path='docs/tags[@name="getAll"]/*' />
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataPaginationModel<BusinessEntityModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(BusinessEntityPaginationModelExample))]
        [Produces("application/json")]
        [HttpGet]
        [Authorize]
        [Route("entities")]
        public async Task<IActionResult> GetAllBusinessEntities([FromQuery] QueryParamModel queryParam)
        {
            return Ok(await _businessEntityService.GetAllBusinessEntities(queryParam));
        }

        /// <summary>
        /// Obtiene todos permisos de usuarios sobre entidades de negocio
        /// </summary>
        /// <param name="queryParam">Parametros para paginación, filtro y ordenación</param>
        /// <returns>Listado de permisos de usuarios sobre entidades de negocio</returns>
        /// <include file='docs_tags.xml' path='docs/tags[@name="getAll"]/*' />
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataPaginationModel<UserBusinessPermModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserBusinessPermPaginationModelExample))]
        [Produces("application/json")]
        [HttpGet]
        [Authorize]
        [Route("usersperms")]
        public async Task<IActionResult> GetAllUserBusinessPerms([FromQuery] QueryParamModel queryParam)
        {
            return Ok(await _userBusinessPermService.GetAllUserBusinessPerms(queryParam));
        }

        /// <summary>
        /// Crea un nuevo permiso de usuario sobre entidade de negocio
        /// </summary>
        /// <param name="userBusinessPerm">Datos del permiso</param>
        /// <returns>Identificador del permiso</returns>
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
        [Route("usersperms")]
        public async Task<IActionResult> CreateUserBusinessPerm([FromBody] UserBusinessPermModel userBusinessPerm)
        {
            var audit = ControllerUtility.GetAuditValues(ControllerContext);
            return Ok(await _userBusinessPermService.AddUserBusinessPerm(userBusinessPerm, audit));
        }

        /// <summary>
        /// Elimina un permiso de usuario sobre entidade de negocio existente
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <param name="businessEntityId">Identificador de la entidad de negocio</param>
        /// <param name="identifier">Identificador de la entidad</param>
        /// <returns>Identificador del permiso</returns>
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
        [Route("usersperms/{userId}/{businessEntityId}/{identifier}")]
        public async Task<IActionResult> DeleteUserBusinessPerm([FromRoute] int userId, [FromRoute] int businessEntityId, [FromRoute] int identifier)
        {
            var audit = ControllerUtility.GetAuditValues(ControllerContext);
            return Ok(await _userBusinessPermService.DeleteUserBusinessPerm(userId, businessEntityId, identifier, audit));
        }
    }
}
