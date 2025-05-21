using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.Application.Registration
{
    [ExcludeFromCodeCoverage]
    public static class ConfigurationManager
    {
        public static IConfiguration? Configuration { get; set; }

        public static bool IsFunction
        {
            get
            {
                return Configuration != null && !string.IsNullOrEmpty(Configuration["IsFunction"]) && bool.Parse(Configuration["IsFunction"]!);
            }
        }

        public static string Separator
        {
            get
            {
                return Configuration != null && !string.IsNullOrEmpty(Configuration["Separator"]) ? Configuration["Separator"]! : ":";
            }
        }

        #region ConnectionStrings

        public static string FactoryDB
        {
            get
            {
                return Configuration != null ? Configuration["ConnectionStrings:Base"]! : string.Empty;
            }
        }

        public static string LocalDB
        {
            get
            {
                return Configuration != null ? Configuration["ConnectionStrings:Local"]! : string.Empty;
            }
        }

        #endregion ConnectionStrings

        #region Identity

        public static string IdentityClientId
        {
            get
            {
                return Configuration != null ? Configuration[$"Identity{Separator}ClientId"]! : string.Empty;
            }
        }

        public static string IdentityIsLocal
        {
            get
            {
                return Configuration != null ? Configuration[$"Identity{Separator}Local"]! : string.Empty;
            }
        }

        #endregion Identity

        #region WebAPI

        public static string WebAPIName
        {
            get
            {
                return Configuration != null ? Configuration[$"WebAPI{Separator}Name"]! : string.Empty;
            }
        }

        public static string CurrentDB
        {
            get
            {
                if (IsFunction)
                    return Configuration != null ? Configuration[$"WebAPI{Separator}CurrentDB"]! : string.Empty;
                else
                    return Configuration != null ? Configuration.GetConnectionString(Configuration[$"WebAPI{Separator}CurrentDB"]!)! : string.Empty;
            }
        }

        public static string SchemaDB
        {
            get
            {
                return Configuration != null ? Configuration[$"WebAPI{Separator}SchemaDB"]! : string.Empty;
            }
        }

        public static string LongDateFormat
        {
            get
            {
                return Configuration != null ? Configuration[$"WebAPI{Separator}LongDateFormat"]! : string.Empty;
            }
        }

        public static string ShortDateFormat
        {
            get
            {
                return Configuration != null ? Configuration[$"WebAPI{Separator}ShortDateFormat"]! : string.Empty;
            }
        }

        public static string GuionDateFormat
        {
            get
            {
                return Configuration != null ? Configuration[$"WebAPI{Separator}GuionDateFormat"]! : string.Empty;
            }
        }

        public static string DatabaseType
        {
            get
            {
                return Configuration != null ? Configuration[$"WebAPI{Separator}DatabaseType"]! : string.Empty;
            }
        }

        #endregion WebAPI

        #region Azure
        public static string AllowChangePower
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}AllowChangePower"]! : string.Empty;
            }
        }

        public static string ClientId
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}ClientId"]! : string.Empty;
            }
        }

        public static string ClientSecret
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}ClientSecret"]! : string.Empty;
            }
        }

        public static string TenantId
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}TenantId"]! : string.Empty;
            }
        }

        public static string SubscriptionId
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}SubscriptionId"]! : string.Empty;
            }
        }

        public static string ServicePlanBack
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}ServicePlanBack"]! : string.Empty;
            }
        }

        public static string ServicePlanAlgorithm
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}ServicePlanAlgorithm"]! : string.Empty;
            }
        }

        public static string SkuNameUp
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}skuNameUp"]! : string.Empty;
            }
        }

        public static string SkuTierUp
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}SkuTierUp"]! : string.Empty;
            }
        }

        public static string SkuNameDown
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}skuNameDown"]! : string.Empty;
            }
        }

        public static string SkuTierDown
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}SkuTierDown"]! : string.Empty;
            }
        }

        public static string SleepTime
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}sleepTime"]! : string.Empty;
            }
        }

        public static string UrlGraph
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}urlGraph"]! : string.Empty;
            }
        }

        public static string RolCreateSSO
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}rolCreateSSO"]! : string.Empty;
            }
        }

        public static string PasswordHash
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}PasswordHash"]! : string.Empty;
            }
        }

        public static string CreateUserSSO
        {
            get
            {
                return Configuration != null ? Configuration[$"Azure{Separator}createUserSSO"]! : string.Empty;
            }
        }

        #endregion Azure

        #region JwtSettings

        public static string JwtIssuer
        {
            get
            {
                return Configuration != null ? Configuration[$"JwtSettings{Separator}Issuer"]! : string.Empty;
            }
        }

        public static string JwtKey
        {
            get
            {
                return Configuration != null ? Configuration[$"JwtSettings{Separator}Key"]! : string.Empty;
            }
        }

        public static string JwtMinutes
        {
            get
            {
                return Configuration != null ? Configuration[$"JwtSettings{Separator}Minutes"]! : string.Empty;
            }
        }

        public static string JwtAuthType
        {
            get
            {
                return Configuration != null ? Configuration[$"JwtSettings{Separator}AuthType"]! : string.Empty;
            }
        }

        public static string JwtAudience
        {
            get
            {
                return Configuration != null ? Configuration[$"JwtSettings{Separator}Audience"]! : string.Empty;
            }
        }

        #endregion JwtSettings

        #region Swagger

        public static string SwaggerEnabled
        {
            get
            {
                return Configuration != null ? Configuration[$"Swagger{Separator}Enabled"]! : string.Empty;
            }
        }

        #endregion Swagger

        #region Here

        public static string DomainHere
        {
            get
            {
                return Configuration != null ? Configuration[$"Here{Separator}Domain"]! : string.Empty;
            }
        }

        public static string ApiKey
        {
            get
            {
                return Configuration != null ? Configuration[$"Here{Separator}ApiKey"]! : string.Empty;
            }
        }
        #endregion Here
    }
}
