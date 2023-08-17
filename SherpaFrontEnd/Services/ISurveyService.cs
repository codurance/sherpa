using SherpaFrontEnd.Dtos;

namespace SherpaFrontEnd.Services;

public interface ISurveyService
{
    public Task CreateSurvey(CreateSurveyDto createSurveyDto);

    public Task<SurveyWithoutQuestions?> GetSurveyById(Guid id);
}