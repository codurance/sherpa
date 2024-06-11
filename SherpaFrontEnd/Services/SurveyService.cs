using System.Net.Http.Json;
using System.Text.Json;
using SherpaFrontEnd.Dtos;
using SherpaFrontEnd.Dtos.Survey;

namespace SherpaFrontEnd.Services;

public class SurveyService : ISurveyService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string SherpaBackend = "SherpaBackEnd";

    public SurveyService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task CreateSurvey(CreateSurveyDto createSurveyDto)
    {
        var client = _httpClientFactory.CreateClient(SherpaBackend);

        await client.PostAsJsonAsync("/survey", createSurveyDto);
    }

    public async Task<SurveyWithoutQuestions?> GetSurveyWithoutQuestionsById(Guid id)
    {
        var client = _httpClientFactory.CreateClient(SherpaBackend);
        return await client.GetFromJsonAsync<SurveyWithoutQuestions>($"/survey/{id.ToString()}");
    }

    public async Task<List<Survey>?> GetAllSurveysByTeam(Guid teamId)
    {
        var httpClient = _httpClientFactory.CreateClient(SherpaBackend);
        var request = new HttpRequestMessage(HttpMethod.Get, $"/team/{teamId}/surveys");
        var response = await httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Survey>>(
            responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<List<Question>?> GetSurveyQuestionsBySurveyId(Guid surveyId)
    {
        var client = _httpClientFactory.CreateClient(SherpaBackend);
        var request = new HttpRequestMessage(HttpMethod.Get, $"/survey/{surveyId.ToString()}/questions");
        var response = await client.SendAsync(request);

        var responseString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Question>>(
            responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task SubmitSurveyResponse(AnswerSurveyDto answerSurveyDto)
    {
        var client = _httpClientFactory.CreateClient(SherpaBackend);

        var requestUri = $"/survey/{answerSurveyDto.SurveyId}/team-members/{answerSurveyDto.TeamMemberId}";
        var response = await client.PostAsJsonAsync(requestUri, answerSurveyDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task LaunchSurvey(Guid surveyId)
    {
        var client = _httpClientFactory.CreateClient(SherpaBackend);

        var launchSurveyDto = new LaunchSurveyDto(surveyId);
        var response = await client.PostAsJsonAsync("/survey-notifications", launchSurveyDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task<SurveyNotification?> GetSurveyNotificationById(Guid surveyNotificationId)
    {
        var client = _httpClientFactory.CreateClient(SherpaBackend);

        var httpRequestMessage =
            new HttpRequestMessage(HttpMethod.Get, $"/survey-notifications/{surveyNotificationId}");

        var httpResponseMessage = await client.SendAsync(httpRequestMessage);
        var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<SurveyNotification>(responseString,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
}