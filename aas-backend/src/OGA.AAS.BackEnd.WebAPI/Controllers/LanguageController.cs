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
    [Route("languages")]
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        /// <summary>
        /// Obtiene todos los idiomas disponibles
        /// </summary>
        /// <param name="queryParam">Parametros para paginación, filtro y ordenación</param>
        /// <returns>Listado de Idiomas</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataPaginationModel<LanguageModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LanguagePaginationModelExample))]
        [Produces("application/json")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "ApiKey")]
        [Route("")]
        public async Task<IActionResult> GetAllLanguages([FromQuery] QueryParamModel queryParam)
        {
            return Ok(await _languageService.GetAllAsync(queryParam));
        }
    }
}
