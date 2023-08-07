using System.Net;
using System.Text.Json;
using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public class TeamServiceHttpClient : ITeamDataService
{
    private const string Sherpabackend = "SherpaBackEnd";
    private readonly IHttpClientFactory _clientFactory;

    public TeamServiceHttpClient(IHttpClientFactory httpClientFactory)
    {
        _clientFactory = httpClientFactory;
    }

    public async Task<List<Team>?> GetAllTeams()
    {
        var client = _clientFactory.CreateClient(Sherpabackend);
        var request = new HttpRequestMessage(HttpMethod.Get, "/team");
        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Team>>(
            responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<Team?> GetTeamById(Guid guid)
    {
        var client = _clientFactory.CreateClient(Sherpabackend);
        var request = new HttpRequestMessage(HttpMethod.Get, $"/team/{guid}");
        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Team>(
            responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<HttpStatusCode> DeleteTeam(Guid guid)
    {
        var httpClient = _clientFactory.CreateClient(Sherpabackend);
        var response = await httpClient.DeleteAsync($"/team/{guid.ToString()}");
        response.EnsureSuccessStatusCode();
        return response.StatusCode;
    }

    public async Task PutTeam(Team team)
    {
        var teamToUpdate =
            JsonSerializer.Serialize(team, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var httpClient = _clientFactory.CreateClient(Sherpabackend);
        var response = await httpClient.PutAsync($"/team/{team.Id.ToString()}",
            new StringContent(teamToUpdate, System.Text.Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
    }

    public async Task AddTeam(Team team)
    {
        var teamToAdd =
            JsonSerializer.Serialize(team,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var client = _clientFactory.CreateClient(Sherpabackend);
        var response = await client.PostAsync("/team", 
            new StringContent(teamToAdd, System.Text.Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
    }
}