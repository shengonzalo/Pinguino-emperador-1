using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.AAS.BackEnd.WebAPI.Controllers.Swagger;
using OGA.Core.BackEnd.Domain.Models;
using OGA.Core.BackEnd.WebAPI.Middleware.HttpResponseException;
using Swashbuckle.AspNetCore.Filters;

namespace OGA.AAS.BackEnd.WebAPI.Controllers
{
    [ApiController]
    [Route("identifiertypes")]
    public class IdentifierTypeController : Controller
    {
        private readonly IIdentifierTypeService _identifierTypeService;

        public IdentifierTypeController(IIdentifierTypeService identifierTypeService)
        {
            _identifierTypeService = identifierTypeService;
        }

        /// <summary>
        /// Obtiene todos los tipos de identificador disponibles
        /// </summary>
        /// <param name="queryParam">Parametros para paginación, filtro y ordenación</param>
        /// <returns>Listado de identificadores</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataPaginationModel<IdentifierTypeModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(IdentifierTypePaginationModelExample))]
        [Produces("application/json")]
        [HttpGet]
        //[Authorize]
        [Route("")]
        public async Task<IActionResult> GetAllIdentifierTypes([FromQuery] QueryParamModel queryParam)
        {
            return Ok(await _identifierTypeService.GetAllAsync(queryParam));
        }
    }
}
