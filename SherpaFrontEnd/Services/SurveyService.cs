using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using SherpaFrontEnd.Dtos;
using SherpaFrontEnd.Dtos.Survey;

namespace SherpaFrontEnd.Services;

public class SurveyService : ISurveyService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAuthService _authService;
    private const string SherpaBackend = "SherpaBackEnd";

    public SurveyService(IHttpClientFactory httpClientFactory, IAuthService authService)
    {
        _httpClientFactory = httpClientFactory;
        _authService = authService;
    }

    public async Task CreateSurvey(CreateSurveyDto createSurveyDto)
    {
        var client = _httpClientFactory.CreateClient(SherpaBackend);

        var request = new HttpRequestMessage(HttpMethod.Post, "/survey");
        var serializedCreateSurveyDto = JsonSerializer.Serialize(createSurveyDto, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        request.Content = new StringContent(serializedCreateSurveyDto, System.Text.Encoding.UTF8, "application/json");
        request = await _authService.DecorateWithToken(request);
        
        await client.SendAsync(request);
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
        request = await _authService.DecorateWithToken(request);
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

    public async Task<byte[]> DownloadSurveyResponses(Guid surveyId)
    {
        var client = _httpClientFactory.CreateClient(SherpaBackend);
        var request = new HttpRequestMessage(HttpMethod.Get, $"/survey/{surveyId}/responses");
        request = await _authService.DecorateWithToken(request);
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsByteArrayAsync();
    }
}