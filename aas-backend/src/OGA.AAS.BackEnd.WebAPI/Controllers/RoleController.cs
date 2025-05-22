using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.AAS.BackEnd.WebAPI.Controllers.Swagger;
using OGA.Core.BackEnd.Domain.Models;
using OGA.Core.BackEnd.WebAPI.Middleware.HttpResponseException;
using OGA.Core.BackEnd.WebAPI.Utilities;
using Swashbuckle.AspNetCore.Filters;

namespace OGA.AAS.BackEnd.WebAPI.Controllers
{
    /// <summary>
    /// Se encarga de la gestión de los roles
    /// </summary>
    [ApiController]
    [Route("roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Obtiene todos los roles
        /// </summary>
        /// <param name="queryParam">Parametros para paginación, filtro y ordenación</param>
        /// <returns>Listado de roles</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="403">Se lanzará esta respuesta cuando el acceso al recurso esté prohibido.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataPaginationModel<RoleModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RolePaginationModelExample))]
        [Produces("application/json")]
        [HttpGet]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> GetAllRoles([FromQuery] QueryParamModel queryParam)
        {
            return Ok(await _roleService.GetAllAsync(queryParam));
        }

        /// <summary>
        /// Obtiene un rol filtrando por su identificador
        /// </summary>
        /// <param name="roleId">Identificador del rol</param>
        /// <returns>Rol</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="403">Se lanzará esta respuesta cuando el acceso al recurso esté prohibido.</response>
        /// <response code="404">Se lanzará esta respuesta cuando los datos de la request no hayan sido encontrados.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleModelExample))]
        [Produces("application/json")]
        [HttpGet]
        [Authorize]
        [Route("{roleId}")]
        public async Task<IActionResult> GetRoleById([FromRoute] int roleId)
        {
            return Ok(await _roleService.GetByIdAsync(roleId));
        }

        /// <summary>
        /// Crea un nuevo rol
        /// </summary>
        /// <param name="role">Datos del rol</param>
        /// <returns>Identificador del rol</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="400">Se lanzará esta respuesta cuando los datos de la request no sean correctos.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="403">Se lanzará esta respuesta cuando el acceso al recurso esté prohibido.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [Produces("application/json")]
        [HttpPost]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> CreateRole([FromBody] RoleModel role)
        {
            var audit = ControllerUtility.GetAuditValues(ControllerContext);
            return Ok(await _roleService.AddAsync(role, audit));
        }

        /// <summary>
        /// Actualiza un rol existente
        /// </summary>
        /// <param name="roleId">Identificador del rol</param>
        /// <param name="role">Datos del rol</param>
        /// <returns>Identificador del rol</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="400">Se lanzará esta respuesta cuando los datos de la request no sean correctos.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="403">Se lanzará esta respuesta cuando el acceso al recurso esté prohibido.</response>
        /// <response code="404">Se lanzará esta respuesta cuando los datos de la request no hayan sido encontrados.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [Produces("application/json")]
        [HttpPut]
        [Authorize]
        [Route("{roleId}")]
        public async Task<IActionResult> UpdateRole([FromRoute] int roleId, [FromBody] RoleModel role)
        {
            var audit = ControllerUtility.GetAuditValues(ControllerContext);
            return Ok(await _roleService.UpdateAsync(roleId, role, audit));
        }

        /// <summary>
        /// Elimina un rol existente
        /// </summary>
        /// <param name="roleId">Identificador del rol</param>
        /// <returns>Identificador del rol</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="400">Se lanzará esta respuesta cuando los datos de la request no sean correctos.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="403">Se lanzará esta respuesta cuando el acceso al recurso esté prohibido.</response>
        /// <response code="404">Se lanzará esta respuesta cuando los datos de la request no hayan sido encontrados.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [Produces("application/json")]
        [HttpDelete]
        [Authorize]
        [Route("{roleId}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int roleId)
        {
            var audit = ControllerUtility.GetAuditValues(ControllerContext);
            return Ok(await _roleService.DeleteRole(roleId, audit));
        }
    }
}
