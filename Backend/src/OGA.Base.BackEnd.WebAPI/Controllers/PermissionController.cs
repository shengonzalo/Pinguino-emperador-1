using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Base.BackEnd.WebAPI.Controllers.Swagger;
using OGA.Core.BackEnd.Domain.Models;
using OGA.Core.BackEnd.WebAPI.Middleware.HttpResponseException;
using OGA.Core.BackEnd.WebAPI.Utilities;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;

namespace OGA.Base.BackEnd.WebAPI.Controllers
{
    /// <summary>
    /// Se encarga de la gestión los permisos de los usuarios
    /// </summary>
    [ApiController]
    [Route("permissions")]
    public class PermissionController(IPermissionService permissionService) : ControllerBase
    {
        private readonly IPermissionService _permissionService = permissionService;

        /// <summary>
        /// Obtiene los permisos de un usuario sobre un recurso
        /// </summary>
        /// <returns>Permisos del usuario</returns>
        /// <include file='docs_tags.xml' path='docs/tags[@name="getAll"]/*' />
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserPermissionModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserPermissionModelExample))]
        [Produces("application/json")]
        [HttpGet]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> GetUserPermissions([FromQuery] int resourceId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _permissionService.GetUserPermissions(resourceId, email!));
        }

        /// <summary>
        /// Obtiene todos los grupos de permisos
        /// </summary>
        /// <param name="queryParam">Parametros para paginación, filtro y ordenación</param>
        /// <returns>Listado de grupos de permisos</returns>
        /// <response code="200">Operación ejecutada con éxito.</response>
        /// <response code="401">Se lanzará esta respuesta cuando la autorización haya sido denegada.</response>
        /// <response code="403">Se lanzará esta respuesta cuando el acceso al recurso esté prohibido.</response>
        /// <response code="500">Se lanzará esta respuesta cuando el servidor sea incapaz de procesar la petición.</response>
        /// <response code="503">Se lanzará esta respuesta cuando el servicio no esté disponible.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataPaginationModel<GroupModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GroupPaginationModelExample))]
        [Produces("application/json")]
        [HttpGet]
        [Authorize]
        [Route("groups")]
        public async Task<IActionResult> GetAllGroups([FromQuery] QueryParamModel queryParam)
        {
            return Ok(await _permissionService.GetAllGroups(queryParam));
        }

        /// <summary>
        /// Actualiza los grupos de permisos asignados a los roles
        /// </summary>
        /// <param name="rolePermGroup">Datos de los roles y grupos de permisos</param>
        /// <returns>Resultado de la operación</returns>
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
        [Authorize(Roles = "1")]
        [Route("groups/roles")]
        public async Task<IActionResult> UpdateRolePermGroup([FromBody] IEnumerable<RolePermissionGroupModel> rolePermGroup)
        {
            var audit = ControllerUtility.GetAuditValues(ControllerContext);
            return Ok(await _permissionService.UpdateRolePermGroup(rolePermGroup, audit));
        }
    }
}
