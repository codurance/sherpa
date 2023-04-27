using System.Net;
using System.Text.Json;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.ViewModel;

namespace SherpaFrontEnd.Services;

public class AssessmentsServiceHttpClient : IAssessmentsDataService
{
    private const string Sherpabackend = "SherpaBackEnd";
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;

    public AssessmentsServiceHttpClient(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _httpClient = _clientFactory.CreateClient(Sherpabackend);
    }

    public async Task<List<Assessment>?> GetAssessments()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/assessments");
        var response = await _httpClient.SendAsync(request);
        
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            
            Console.WriteLine("raw response: " + responseString);

            return JsonSerializer.Deserialize<List<Assessment>>(
                responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });    
        }

        return new List<Assessment>();
    }

    public async Task<List<SurveyTemplate>?> GetTemplates()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/assessments/templates");
        var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<SurveyTemplate>>(
                responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });    
        }

        return new List<SurveyTemplate>();
    }

    public async Task<Assessment?> AddAssessment(Guid groupId, Guid templateId, string name)
    {
        var assessment = new AssessmentToAdd(groupId, templateId, name);
        
        var assessmentToAdd =
            JsonSerializer.Serialize(assessment,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        
        var response = await _httpClient.PostAsync("/assessments", 
            new StringContent(assessmentToAdd, System.Text.Encoding.UTF8, "application/json"));
        
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Assessment>(
                responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });    
        }

        return null;
    }

    public async Task<Assessment?> PutAssessment(Assessment assessment)
    {
        var assessmentToUpdate = JsonSerializer.Serialize(assessment, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        var client = _clientFactory.CreateClient(Sherpabackend);
        var response = await(client.PutAsync($"/assessments",
            new StringContent(assessmentToUpdate, System.Text.Encoding.UTF8, "application/json")));
        
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Assessment>(
                responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });    
        }

        return null;
    }
}