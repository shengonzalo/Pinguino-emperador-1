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
    /// Se encarga de la gestión de los roles
    /// </summary>
    [ApiController]
    [Route("roles")]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        /// <summary>
        /// Obtiene todos los roles
        /// </summary>
        /// <param name="queryParam">Parametros para paginación, filtro y ordenación</param>
        /// <returns>Listado de roles</returns>
        /// <include file='docs_tags.xml' path='docs/tags[@name="getAll"]/*' />
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
        /// <include file='docs_tags.xml' path='docs/tags[@name="getById"]/*' />
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
        [AuditFilter("Role", AuditEnum.Create, typeof(RoleModel))]
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
        [Route("{roleId}")]
        [AuditFilter("Role", AuditEnum.Update, typeof(RoleModel))]
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
        [Route("{roleId}")]
        [AuditFilter("Role", AuditEnum.Delete, typeof(RoleModel))]
        public async Task<IActionResult> DeleteRole([FromRoute] int roleId)
        {
            var audit = ControllerUtility.GetAuditValues(ControllerContext);
            return Ok(await _roleService.DeleteRole(roleId, audit));
        }
    }
}
