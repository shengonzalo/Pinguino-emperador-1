using System.Text.Json.Serialization;

namespace OGA.Base.BackEnd.Domain.Models.Internal.Graph;

/// <summary>
/// Representa un usuario de Microsoft Graph API.
/// </summary>
public class MicrosoftUserModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("givenName")]
    public string GivenName { get; set; } = string.Empty;

    [JsonPropertyName("surname")]
    public string Surname { get; set; } = string.Empty;

    [JsonPropertyName("jobTitle")]
    public string JobTitle { get; set; } = string.Empty;

    [JsonPropertyName("mail")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("userPrincipalName")]
    public string UserPrincipalName { get; set; } = string.Empty;

    [JsonPropertyName("businessPhones")]
    public List<string> BusinessPhones { get; set; } = new();

    [JsonPropertyName("mobilePhone")]
    public string MobilePhone { get; set; } = string.Empty;

    [JsonPropertyName("officeLocation")]
    public string OfficeLocation { get; set; } = string.Empty;

    [JsonPropertyName("preferredLanguage")]
    public string? PreferredLanguage { get; set; }
}
