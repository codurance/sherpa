using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Survey.Domain;
using SherpaBackEnd.Survey.Infrastructure.Http.Dto;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Template.Domain;
using SherpaBackEnd.Template.Infrastructure.Http.Dto;
using ISurveyRepository = SherpaBackEnd.Survey.Domain.Persistence.ISurveyRepository;

namespace SherpaBackEnd.Survey.Application;

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

        var survey = new Domain.Survey(createSurveyDto.SurveyId, new User.Domain.User(DefaultUserId, "Lucia"), SurveyStatus.Draft,
            createSurveyDto.Deadline, createSurveyDto.Title, createSurveyDto.Description, new List<SurveyResponse>(), team,
            template);

        await _surveyRepository.CreateSurvey(survey);
    }

    public async Task<IEnumerable<Domain.Survey>> GetAllSurveysFromTeam(Guid teamId)
    {
        try
        {
            return await _surveyRepository.GetAllSurveysFromTeam(teamId);
        }
        catch (Exception error)
        {
            throw new ConnectionToRepositoryUnsuccessfulException(error.Message, error);
        }
    }

    public async Task<SurveyWithoutQuestions> GetSurveyWithoutQuestionsById(Guid expectedSurveyId)
    {
        var surveyById = await _surveyRepository.GetSurveyById(expectedSurveyId);

        if (surveyById == null)
        {
            throw new NotFoundException("Survey not found");
        }

        return new SurveyWithoutQuestions(surveyById.Id, surveyById.Coach, surveyById.SurveyStatus, surveyById.Deadline,
            surveyById.Title, surveyById.Description, surveyById.Responses, surveyById.Team,
            new TemplateWithoutQuestions(surveyById.Template.Name, surveyById.Template.MinutesToComplete));
    }

    public async Task<IEnumerable<IQuestion>> GetSurveyQuestionsBySurveyId(Guid expectedSurveyId)
    {
        var surveyById = await _surveyRepository.GetSurveyById(expectedSurveyId);
        return surveyById.Template.Questions;
    }

    public Task AnswerSurvey(AnswerSurveyDto answerSurveyDto)
    {
        return null;
    }
}