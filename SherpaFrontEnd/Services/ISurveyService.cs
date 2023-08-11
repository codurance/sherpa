using SherpaFrontEnd.Dtos;

namespace BlazorApp.Tests.Pages;

public interface ISurveyService
{
    public Task CreateSurvey(CreateSurveyDto createSurveyDto);

    public Task<SurveyWithoutQuestions> GetSurveyById(string id);
}