using System.Text.Json;
using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public class GroupServiceHttpClient : IGroupDataService
{
    private const string Sherpabackend = "SherpaBackEnd";
    private readonly IHttpClientFactory _clientFactory;

    public GroupServiceHttpClient(IHttpClientFactory httpClientFactory)
    {
        _clientFactory = httpClientFactory;
    }

    public async Task<List<Group>?> GetGroups()
    {
        var client = _clientFactory.CreateClient(Sherpabackend);
        var request = new HttpRequestMessage(HttpMethod.Get, "/groups");
        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<List<Group>>(
            responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task DeleteGroup(Guid guid)
    {
        var httpClient = _clientFactory.CreateClient(Sherpabackend);
        var response = await httpClient.DeleteAsync($"/groups/{guid.ToString()}");
        response.EnsureSuccessStatusCode();
    }
}