using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OGA.Base.BackEnd.Infrastructure.Context;
using OGA.Base.BackEnd.WebAPI.Controllers.Swagger;
using OGA.Core.BackEnd.Domain.Enums;
using OGA.Core.BackEnd.Infrastructure.Extensions;
using OGA.Core.BackEnd.WebAPI.Middleware.HttpResponseException;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.WebAPI.Controllers
{
    /// <summary>
    /// Se encarga de la obtención del estado de conectividad del Api
    /// </summary>
    [ApiController]
    [Route("healthcheck")]
    [ExcludeFromCodeCoverage]
    public class HealthCheckController : ControllerBase
    {
        /// <summary>
        /// Obtiene datos sobre la versión del Api
        /// </summary>
        /// <returns>Objeto con nombre del Api y su versión</returns>
        /// <include file='docs_tags.xml' path='docs/tags[@name="healthcheck"]/*' />
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(HealthCheckModelExample))]
        [Produces("application/json")]
        [HttpGet]
        [AllowAnonymous]
        [Route("version")]
        public IActionResult GetAppVersion()
        {
            return Ok(DateTime.UtcNow.ToString(Application.Registration.ConfigurationManager.LongDateFormat));
        }

        /// <summary>
        /// Obtiene el estado de la base de datos
        /// </summary>
        /// <returns>Fecha actual o mensaje de error de base de datos</returns>
        /// <include file='docs_tags.xml' path='docs/tags[@name="healthcheck"]/*' />
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpResponseException))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(HttpResponseException))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(HealthCheckModelExample))]
        [Produces("application/json")]
        [HttpGet]
        [AllowAnonymous]
        [Route("versiondb")]
        public IActionResult GetAppVersionDb()
        {
            var database = string.IsNullOrEmpty(Application.Registration.ConfigurationManager.DatabaseType) ? (int)DatabaseEnum.SqlServer :
                int.Parse(Application.Registration.ConfigurationManager.DatabaseType);

            using DbContext context = new BaseDbContext(new DbContextOptions<BaseDbContext>());
            object[] parameters = [];
            string sql = database == (int)DatabaseEnum.PostgreSql ? "SELECT NOW()" : "SELECT GETDATE()";
            var dbResponse = DbContextCommandExtension.ExecuteScalarAsync<DateTime>(context, sql, parameters).Result;
            return Ok(dbResponse.ToString(Application.Registration.ConfigurationManager.LongDateFormat));
        }
    }
}
