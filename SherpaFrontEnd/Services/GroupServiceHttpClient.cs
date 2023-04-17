using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public class GroupServiceHttpClient : IGroupDataService
{
    private readonly IHttpClientFactory _clientFactory;

    public GroupServiceHttpClient(IHttpClientFactory httpClientFactory)
    {
        _clientFactory = httpClientFactory;
    }

    public async Task<List<Group>?> getGroups()
    {
        var client = _clientFactory.CreateClient("SherpaBackEnd");
        var request = new HttpRequestMessage(HttpMethod.Get, "/groups");
        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<List<Group>>(
            responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}