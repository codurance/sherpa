using System.Text.Json;
using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public class AssessmentsServiceHttpClient : IAssessmentsDataService
{
    private const string Sherpabackend = "SherpaBackEnd";
    private readonly IHttpClientFactory _clientFactory;

    public AssessmentsServiceHttpClient(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<List<Assessment>?> GetAssessments()
    {
        var client = _clientFactory.CreateClient(Sherpabackend);
        var request = new HttpRequestMessage(HttpMethod.Get, "/assessments");
        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Assessment>>(
            responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}