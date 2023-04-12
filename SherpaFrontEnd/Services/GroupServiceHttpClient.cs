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

    public async Task<List<Group>> getGroups()
    {
        var client = _clientFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7281/groups");
        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var groups = (List<Group>?)await JsonSerializer.DeserializeAsync<IEnumerable<Group>>(
                responseStream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return groups;
        }

        return null;
    }
}