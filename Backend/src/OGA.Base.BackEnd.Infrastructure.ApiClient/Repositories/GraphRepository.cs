using OGA.Base.BackEnd.Domain.ApiClient;
using System.Net.Http.Headers;
using System.Text.Json;
using OGA.Base.BackEnd.Domain.Models.Internal.Graph;
using OGA.Base.BackEnd.Application.Registration;

namespace OGA.Base.BackEnd.Infrastructure.ApiClient.Repositories;

public class GraphRepository(IHttpClientFactory httpClientFactory) : IGraphRepository
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    /// <summary>
    /// Valida el token de Microsoft con la API de Graph
    /// </summary>
    public async Task<MicrosoftUserModel?> ValidateMicrosoftTokenAsync(string token)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, ConfigurationManager.UrlGraph);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MicrosoftUserModel>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch
        {
            return null;
        }
    }
}
