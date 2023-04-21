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

    public async Task PutGroup(Group group)
    {
        var groupToUpdate =
            JsonSerializer.Serialize(group, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var httpClient = _clientFactory.CreateClient(Sherpabackend);
        var response = await httpClient.PutAsync($"/groups/{group.Id.ToString()}",
            new StringContent(groupToUpdate, System.Text.Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
    }

    public async Task AddGroup(Group group)
    {
        var groupToAdd =
            JsonSerializer.Serialize(group,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var client = _clientFactory.CreateClient(Sherpabackend);
        Console.WriteLine("httpclient "+groupToAdd);
        var response = await client.PostAsync("/groups", 
            new StringContent(groupToAdd, System.Text.Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
    }
}