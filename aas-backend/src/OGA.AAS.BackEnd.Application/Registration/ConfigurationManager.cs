using Microsoft.Extensions.Configuration;
using OGA.AAS.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Registration
{
    public class ConfigurationManager
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

        public static string LogisticsDB
        {
            get
            {
                return Configuration != null ? Configuration[$"ConnectionStrings:Logistics"]! : string.Empty;
            }
        }

        public static string LocalDB
        {
            get
            {
                return Configuration != null ? Configuration[$"ConnectionStrings:Local"]! : string.Empty;
            }
        }

        #endregion ConnectionStrings

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

        public static string DatabaseType
        {
            get
            {
                return Configuration != null ? Configuration[$"WebAPI{Separator}DatabaseType"]! : string.Empty;
            }
        }

        #endregion WebAPI

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

        public static string JwtAudience
        {
            get
            {
                return Configuration != null ? Configuration[$"JwtSettings{Separator}Audience"]! : string.Empty;
            }
        }

        public static string JwtAuthType
        {
            get
            {
                return Configuration != null ? Configuration[$"JwtSettings{Separator}AuthType"]! : string.Empty;
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

        #region ConfigAuditUserKeyModel

        public static ConfigAuditUserKeyModel ConfigAuditUserKey
        {
            get
            {
                return Configuration != null ? Configuration.GetSection("ConfigAuditUserKey").Get<ConfigAuditUserKeyModel>()! : new ConfigAuditUserKeyModel();
            }
        }

        #endregion ConfigAuditUserKeyModel

        #region CommunicationService

        public static string CommunicationServiceUrl
        {
            get
            {
                return Configuration != null ? Configuration[$"CommunicationService{Separator}BaseUrl"]! : string.Empty;
            }
        }

        public static string CommunicationServiceSender
        {
            get
            {
                return Configuration != null ? Configuration[$"CommunicationService{Separator}Sender"]! : string.Empty;
            }
        }

        public static string CommunicationServiceRecipient
        {
            get
            {
                return Configuration != null ? Configuration[$"CommunicationService{Separator}Recipient"]! : string.Empty;
            }
        }

        public static string CommunicationService2FALength
        {
            get
            {
                return Configuration != null ? Configuration[$"CommunicationService{Separator}2FALength"]! : string.Empty;
            }
        }

        public static string CommunicationService2FADuration
        {
            get
            {
                return Configuration != null ? Configuration[$"CommunicationService{Separator}2FADuration"]! : string.Empty;
            }
        }

        #endregion CommunicationService
    }
}
