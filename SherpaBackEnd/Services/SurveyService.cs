using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Repositories;
using ISurveyRepository = SherpaBackEnd.Model.ISurveyRepository;

namespace SherpaBackEnd.Services;

public class SurveyService : ISurveyService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly ITemplateRepository _templateRepository;
    public readonly Guid DefaultUserId = Guid.NewGuid();

    public SurveyService(ISurveyRepository surveyRepository, ITeamRepository teamRepository,
        ITemplateRepository templateRepository)
    {
        _surveyRepository = surveyRepository;
        _teamRepository = teamRepository;
        _templateRepository = templateRepository;
    }

    public async Task CreateSurvey(CreateSurveyDto createSurveyDto)
    {
        var team = await _teamRepository.GetTeamByIdAsync(createSurveyDto.TeamId);
        var template = await _templateRepository.GetTemplateByName(createSurveyDto.TemplateName);
        
        if (team == null) throw new NotFoundException("Team not found");
        if (template == null) throw new NotFoundException("Template not found");
        
        var survey = new Survey(createSurveyDto.SurveyId, new User(DefaultUserId, "Lucia"), Status.Draft, createSurveyDto.Deadline, createSurveyDto.Title, createSurveyDto.Description, Array.Empty<Response>(), team, template);
        
        await _surveyRepository.CreateSurvey(survey);
    }

    public Task<IEnumerable<Survey>> GetAllSurveys()
    {
        throw new NotImplementedException();
    }

    public async Task<Survey> GetSurveyById(Guid expectedSurveyId)
    {
        return await _surveyRepository.GetSurveyById(expectedSurveyId);
    }
}