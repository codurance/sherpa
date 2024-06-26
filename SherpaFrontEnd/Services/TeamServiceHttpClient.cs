using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public class TeamServiceHttpClient : ITeamDataService
{
    private const string Sherpabackend = "SherpaBackEnd";
    private readonly IHttpClientFactory _clientFactory;
    private readonly IAuthService _authService;

    public TeamServiceHttpClient(IHttpClientFactory httpClientFactory, IAuthService authService)
    {
        _clientFactory = httpClientFactory;
        _authService = authService;
    }

    public async Task<List<Team>> GetAllTeams()
    {
        var client = _clientFactory.CreateClient(Sherpabackend);
        var request = new HttpRequestMessage(HttpMethod.Get, "/team");
        request = await _authService.DecorateWithToken(request);
        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Team>>(
            responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<Team> GetTeamById(Guid guid)
    {
        var client = _clientFactory.CreateClient(Sherpabackend);
        var request = new HttpRequestMessage(HttpMethod.Get, $"/team/{guid}");
        request = await _authService.DecorateWithToken(request);
        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Team>(
            responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<HttpStatusCode> DeleteTeam(Guid guid)
    {
        var httpClient = _clientFactory.CreateClient(Sherpabackend);
        var request = new HttpRequestMessage(HttpMethod.Delete, $"/team/{guid.ToString()}");
        request = await _authService.DecorateWithToken(request);

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return response.StatusCode;
    }

    public async Task PutTeam(Team team)
    {
        var teamToUpdate =
            JsonSerializer.Serialize(team, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var httpClient = _clientFactory.CreateClient(Sherpabackend);
        var request = new HttpRequestMessage(HttpMethod.Put, $"/team/{team.Id.ToString()}");
        request.Content = new StringContent(teamToUpdate, System.Text.Encoding.UTF8, "application/json");
        request = await _authService.DecorateWithToken(request);

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public async Task AddTeam(Team team)
    {
        var teamToAdd =
            JsonSerializer.Serialize(team,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var client = _clientFactory.CreateClient(Sherpabackend);
        var request = new HttpRequestMessage(HttpMethod.Post, "/team");
        request.Content = new StringContent(teamToAdd, System.Text.Encoding.UTF8, "application/json");
        request = await _authService.DecorateWithToken(request);
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public async Task AddTeamMember(AddTeamMemberDto addTeamMemberDto)
    {
        var client = _clientFactory.CreateClient(Sherpabackend);
        var request = new HttpRequestMessage(HttpMethod.Patch, $"/team/{addTeamMemberDto.TeamId}/members");
        request.Content = new StringContent(
            JsonSerializer.Serialize(addTeamMemberDto,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
            System.Text.Encoding.UTF8, "application/json");
        request = await _authService.DecorateWithToken(request);

        await client.SendAsync(request);
    }
}